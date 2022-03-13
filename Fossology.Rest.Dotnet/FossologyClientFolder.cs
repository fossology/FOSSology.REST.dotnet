// ---------------------------------------------------------------------------
// <copyright file="FossologyClientFolder.cs" company="Tethys">
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
        /// <summary>
        /// Gets the folder with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="groupName">The group name to choose.</param>
        /// <returns>A <see cref="Folder"/> object.</returns>
        public Folder GetFolder(int id, string groupName = "")
        {
            Log.Debug($"Getting folder {id}...");

            var request = new RestRequest(this.Url + $"/folders/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            var folder = JsonConvert.DeserializeObject<Folder>(
                response.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    });
            return folder;
        } // GetFolder()

        /// <summary>
        /// Gets the list of all folders.
        /// </summary>
        /// <param name="groupName">The group name to choose.</param>
        /// <returns>A list of <see cref="Folder"/> objects.</returns>
        public IReadOnlyList<Folder> GetFolderList(string groupName = "")
        {
            Log.Debug("Getting list of folder...");

            var request = new RestRequest(this.Url + $"/folders", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            var list = JsonConvert.DeserializeObject<List<Folder>>(
                response.Content,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            return list;
        } // GetFolderList()

        /// <summary>
        /// Creates a new folder.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="parentFolder">The parent folder.</param>
        /// <param name="description">The folder description (optional).</param>
        /// <param name="groupName">The group name to choose (optional).</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        /// <remarks>
        /// The message property of the result contains the folder id
        /// which is needed for further operations.
        /// </remarks>>
        public Result CreateFolder(string folderName, int parentFolder, string description = "", string groupName = "")
        {
            var request = new RestRequest(this.Url + "/folders", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("parentFolder", parentFolder.ToString());
            request.AddHeader("folderName", folderName);
            if (!string.IsNullOrEmpty(description))
            {
                request.AddHeader("description", description);
            } // if

            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var resultRaw = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            Log.Debug($"Folder {result.Message} created.");

            return result;
        } // CreateFolder()

        /// <summary>
        /// Deletes the folder with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="groupName">The group name to choose.</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        public Result DeleteFolder(int id, string groupName = "")
        {
            Log.Debug($"Deleting folder {id}...");

            var request = new RestRequest(this.Url + $"/folders/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            return result;
        } // DeleteFolder()
    } // FossologyClient
}
