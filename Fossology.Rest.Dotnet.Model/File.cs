// ---------------------------------------------------------------------------
// <copyright file="File.cs" company="Tethys">
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
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Fossology file information as returned by searching for files.
    /// </summary>
    public class File
    {
        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        [JsonProperty("hash")]
        public Hash Hash { get; set; }

        /// <summary>
        /// Gets or sets the findings.
        /// </summary>
        [JsonProperty("findings")]
        public Findings Findings { get; set; }

        /// <summary>
        /// Gets or sets the SHA256 hash.
        /// </summary>
        [JsonProperty("uploads")]
#pragma warning disable CA2227 // Collection properties should be read only
        public List<int> Uploads { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Hash}, {this.Findings}, {this.Uploads}, '{this.Message}'";
        }
    } // File
}
