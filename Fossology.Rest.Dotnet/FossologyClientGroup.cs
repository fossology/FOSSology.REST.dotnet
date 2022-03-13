// ---------------------------------------------------------------------------
// <copyright file="FossologyClientGroup.cs" company="Tethys">
//   Copyright (C) 2022 T. Graf
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
        /// Gets the list of all groups.
        /// </summary>
        /// <returns>A list of <see cref="Group"/> objects.</returns>
        public IReadOnlyList<Group> GetGroupList()
        {
            Log.Debug("Getting list of folder...");

            var request = new RestRequest(this.Url + $"/groups", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();

            var response = this.api.Execute(request);
            var list = JsonConvert.DeserializeObject<List<Group>>(
                response.Content,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            return list;
        } // GetGroupList()

#if false // not yet supported by Fossology
        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        /// <remarks>
        /// The message property of the result contains the group id
        /// which is needed for further operations.
        /// </remarks>>
        public Result CreateGroup(string groupName)
        {
            var request = new RestRequest(this.Url + "/groups", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("groupName", groupName);

            var resultRaw = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            Log.Debug($"Group {result.Message} created.");

            return result;
        } // CreateGroup()
#endif
    } // FossologyClient
}
