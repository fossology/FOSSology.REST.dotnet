// ---------------------------------------------------------------------------
// <copyright file="AppInfo.cs" company="Tethys">
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

namespace Fossology.Rest.Dotnet.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// Fossology application information.
    /// </summary>
    public class AppInfo
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the contact information.
        /// </summary>
        [JsonProperty("contact")]
        public string Contact { get; set; }

        /// <summary>
        /// Gets or sets the Fossology license information.
        /// </summary>
        [JsonProperty("license")]
        public FossologyLicense FossologyLicense { get; set; }

        /// <summary>
        /// Gets or sets the Fossology information.
        /// </summary>
        [JsonProperty("fossology")]
        public FossologyInfo FossologyInfo { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Name}, {this.Version}";
        }
    } // AppInfo
}
