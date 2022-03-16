// ---------------------------------------------------------------------------
// <copyright file="FossologyClientSearch.cs" company="Tethys">
//   Copyright (C) 2019-2022 T. Graf
// </copyright>
//
// Licensed under the MIT License.
// SPDX-License-Identifier: MIT
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied.
// ---------------------------------------------------------------------------

namespace Fossology.Rest.Dotnet
{
    using System.Collections.Generic;
    using Fossology.Rest.Dotnet.Model;

    using Newtonsoft.Json;

    using RestSharp;

    using JsonSerializer = RestSharp.Serialization.Json.JsonSerializer;

    /// <summary>
    /// Client for the SW360 REST API.
    /// </summary>
    public partial class FossologyClient
    {
        /// <summary>Search for a specific file.</summary>
        /// <param name="fileName">Filename to find, can contain % as wild-card.</param>
        /// <param name="tag">Tag to find.</param>
        /// <param name="searchType">Limit search, can be directory, containers, <c>allfiles</c>.</param>
        /// <param name="fileSizeMin">The file size minimum.</param>
        /// <param name="fileSizeMax">The file size maximum.</param>
        /// <param name="license">The license.</param>
        /// <param name="copyright">The copyright.</param>
        /// <param name="groupName">The group name to chose while searching.</param>
        /// <returns>A list of <see cref="SearchResult"/> objects.</returns>
        public IReadOnlyList<SearchResult> Search(
            string fileName,
            string tag = null,
            string searchType = "allfiles",
            int fileSizeMin = -1,
            int fileSizeMax = -1,
            string license = null,
            string copyright = null,
            string groupName = "")
        {
            Log.Debug($"Searching for file {fileName}...");

            var request = new RestRequest(this.Url + "/search", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            request.AddHeader("searchType", searchType);
            request.AddHeader("fileName", fileName);

            if (!string.IsNullOrEmpty(tag))
            {
                request.AddHeader("tag", tag);
            } // if

            if (fileSizeMin > -1)
            {
                request.AddHeader("filesizemin", fileSizeMin.ToString());
            } // if

            if (fileSizeMax > -1)
            {
                request.AddHeader("filesizemax", fileSizeMax.ToString());
            } // if

            if (!string.IsNullOrEmpty(license))
            {
                request.AddHeader("license", license);
            } // if

            if (!string.IsNullOrEmpty(license))
            {
                request.AddHeader("copyright", copyright);
            } // if

            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<IReadOnlyList<SearchResult>>(response.Content);

            return result;
        } // Search()

        /// <summary>
        /// Searches for files by hash.
        /// </summary>
        /// <param name="fileHashes">A list of file hashes.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>A raw JSON result.</returns>
        public string SearchForFile(List<Hash> fileHashes, string groupName = "")
        {
            Log.Debug($"Searching for files by hash...");

            var json = JsonConvert.SerializeObject(fileHashes);

            var request = new RestRequest(this.Url + "/filesearch", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            request.AddJsonBody(json);
            var resultRaw = this.api.Execute(request);
            return resultRaw.Content;
        } // SearchForFile()
    } // FossologyClient
}
