// ---------------------------------------------------------------------------
// <copyright file="FossologyClientHealth.cs" company="Tethys">
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
    using Fossology.Rest.Dotnet.Model;

    using Newtonsoft.Json;

    /// <summary>
    /// Client for the SW360 REST API.
    /// </summary>
    public partial class FossologyClient
    {
        /// <summary>
        /// Gets the health information.
        /// </summary>
        /// <returns><see cref="HealthInfo"/>.</returns>
        public HealthInfo GetHealth()
        {
            Log.Debug($"Getting health info...");

            var result = this.api.Get(this.Url + $"/health");
            var info = JsonConvert.DeserializeObject<HealthInfo>(
                result.Content,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            return info;
        } // GetHealth()
    } // FossologyClient
}
