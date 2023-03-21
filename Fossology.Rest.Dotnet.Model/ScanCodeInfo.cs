// ---------------------------------------------------------------------------
// <copyright file="ScanCodeInfo.cs" company="Tethys">
//   Copyright (C) 2023 T. Graf
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
    /// Trigger ScanCode information.
    /// </summary>
    public class ScanCodeInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether to scan for licenses.
        /// </summary>
        [JsonProperty("license")]
        public bool License { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to scan for copyrights.
        /// </summary>
        [JsonProperty("copyright")]
        public bool Copyright { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to scan for emails.
        /// </summary>
        [JsonProperty("email")]
        public bool Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to scan for urls.
        /// </summary>
        [JsonProperty("url")]
        public bool Url { get; set; }
    } // ScanCodeInfo
}
