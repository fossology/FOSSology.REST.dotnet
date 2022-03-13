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

    /// <summary>
    /// Client for the SW360 REST API.
    /// </summary>
    public partial class FossologyClient
    {
        /// <summary>
        /// Gets the folder with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Folder"/> object.</returns>
        public Folder GetFolder(int id)
        {
            Log.Debug($"Getting folder {id}...");

            var result = this.api.Get(this.Url + $"/folders/{id}");
            var folder = JsonConvert.DeserializeObject<Folder>(
                result.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    });
            return folder;
        } // GetFolder()

        /// <summary>
        /// Gets the list of all folders.
        /// </summary>
        /// <returns>A list of <see cref="Folder"/> objects.</returns>
        public IReadOnlyList<Folder> GetFolderList()
        {
            Log.Debug("Getting list of folder...");

            var result = this.api.Get(this.Url + "/folders");
            var list = JsonConvert.DeserializeObject<List<Folder>>(
                result.Content,
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
        /// <returns>An <see cref="Result"/> object.</returns>
        /// <remarks>
        /// The message property of the result contains the folder id
        /// which is needed for further operations.
        /// </remarks>>
        public Result CreateFolder(string folderName, int parentFolder)
        {
            var request = new RestRequest(this.Url + "/folders", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("parentFolder", parentFolder.ToString());
            request.AddHeader("folderName", folderName);

            var resultRaw = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            Log.Debug($"Folder {result.Message} created.");

            return result;
        } // CreateFolder()

        /// <summary>
        /// Deletes the folder with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        public Result DeleteFolder(int id)
        {
            Log.Debug($"Deleting folder {id}...");

            var response = this.api.Delete(this.Url + $"/folders/{id}");
            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            return result;
        } // DeleteFolder()
    } // FossologyClient
}
