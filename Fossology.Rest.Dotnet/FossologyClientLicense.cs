// ---------------------------------------------------------------------------
// <copyright file="FossologyClientLicense.cs" company="Tethys">
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
        /// Gets the list of all licenses.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="kind">The license kind, one of candidate, main, all.</param>
        /// <param name="active">if set to <c>true</c>, return only active licenses.</param>
        /// <param name="groupName">The group name to choose.</param>
        /// <returns>A list of <see cref="License" /> objects.</returns>
        public IReadOnlyList<License> GetLicenseList(
            int page = 1,
            int limit = 100,
            string kind = "all",
            bool active = false,
            string groupName = "")
        {
            Log.Debug("Getting list of licenses...");

            var request = new RestRequest(this.Url + $"/license?kind={kind}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            request.AddHeader("page", page.ToString());
            request.AddHeader("limit", limit.ToString());
            request.AddHeader("active", active.ToString());

            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            var list = JsonConvert.DeserializeObject<List<License>>(
                response.Content,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            return list;
        } // GetLicenseList()

        /// <summary>
        /// Gets the license with the specified short name.
        /// </summary>
        /// <param name="shortName">The license short name.</param>
        /// <param name="groupName">The group name to choose.</param>
        /// <returns>A <see cref="License"/> object.</returns>
        public License GetLicense(string shortName, string groupName = "")
        {
            Log.Debug($"Getting license {shortName}...");

            var request = new RestRequest(this.Url + $"/license/{shortName}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            var license = JsonConvert.DeserializeObject<License>(
                response.Content,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            return license;
        } // GetLicense()

        /// <summary>
        /// Creates a new license.
        /// </summary>
        /// <param name="newLicense">The new license to be created.</param>
        /// <param name="groupName">The group name to choose (optional).</param>
        /// <returns>An <see cref="Result" /> object.</returns>
        /// &gt;
        /// <remarks>The message property of the result contains the folder id
        /// which is needed for further operations.</remarks>
        public Result CreateLicense(License newLicense, string groupName = "")
        {
            var json = JsonConvert.SerializeObject(newLicense);
            var request = new RestRequest(this.Url + "/license", Method.POST);
            request.RequestFormat = DataFormat.Json;
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            request.AddJsonBody(json);

            var resultRaw = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            Log.Debug($"Folder {result.Message} created.");

            return result;
        } // CreateLicense()
    } // FossologyClient
}
