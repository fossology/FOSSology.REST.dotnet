// ---------------------------------------------------------------------------
// <copyright file="FossologyClientInfo.cs" company="Tethys">
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
    using Fossology.Rest.Dotnet.Model;

    using Newtonsoft.Json;

    /// <summary>
    /// Client for the SW360 REST API.
    /// </summary>
    public partial class FossologyClient
    {
        /// <summary>
        /// Gets the Fossology information.
        /// </summary>
        /// <returns><see cref="AppInfo"/>.</returns>
        public AppInfo GetInfo()
        {
            Log.Debug($"Getting Fossology info...");

            var result = this.api.Get(this.Url + "/info");
            var info = JsonConvert.DeserializeObject<AppInfo>(
                result.Content,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            return info;
        } // GetInfo()
    } // FossologyClient
}
