// ---------------------------------------------------------------------------
// <copyright file="SearchHash.cs" company="Tethys">
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
    /// Fossology hash information for searching.
    /// </summary>
    public class SearchHash
    {
        /// <summary>
        /// Gets or sets the SHA1 hash.
        /// </summary>
        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        /// <summary>
        /// Gets or sets the MD5 hash.
        /// </summary>
        [JsonProperty("md5")]
        public string Md5 { get; set; }

        /// <summary>
        /// Gets or sets the SHA256 hash.
        /// </summary>
        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"SHA1={this.Sha1}, MD5={this.Md5}, SHA256={this.Sha256}";
        }
    } // SearchHash
}
