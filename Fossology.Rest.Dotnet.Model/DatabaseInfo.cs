// ---------------------------------------------------------------------------
// <copyright file="DatabaseInfo.cs" company="Tethys">
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
    /// Fossology database information.
    /// </summary>
    public class DatabaseInfo
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Status}";
        }
    } // DatabaseInfo
}
