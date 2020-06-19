// ---------------------------------------------------------------------------
// <copyright file="VersionInfo.cs" company="Tethys">
//   Copyright (C) 2019-2020 T. Graf
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
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// Fossology version information.
    /// </summary>
    public class VersionInfo
    {
        /*
         {
            "version": "string",
            "security": [
                "string"
            ]
         }
         */

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets the supported authentication methods.
        /// </summary>
        [JsonProperty("security")]
        public List<string> Security { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder(100);
            sb.Append(this.Version);
            sb.Append(": ");
            sb.Append(Support.ListToString(this.Security));

            return sb.ToString();
        } // ToString()
    }
}
