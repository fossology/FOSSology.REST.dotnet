// ---------------------------------------------------------------------------
// <copyright file="License.cs" company="Tethys">
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
    /// Fossology license information.
    /// </summary>
    public class License
    {
        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        [JsonProperty("fullName")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the license text.
        /// </summary>
        [JsonProperty("text")]
        public string LicenseText { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the risk.
        /// </summary>
        [JsonProperty("risk")]
        public int Risk { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a license candidate.
        /// </summary>
        [JsonProperty("isCandidate")]
        public bool IsCandidate { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.ShortName}: {this.FullName}, parent={this.Risk}, {this.Url}, {this.IsCandidate}, '{this.LicenseText}'";
        }
    } // License
}
