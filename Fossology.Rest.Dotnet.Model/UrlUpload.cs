// ---------------------------------------------------------------------------
// <copyright file="UrlUpload.cs" company="Tethys">
//   Copyright (C) 2020 T. Graf
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
    /// Information to create an upload from a URL.
    /// </summary>
    public class UrlUpload
    {
        /// <summary>
        /// Gets or sets the URL for file/folder to be uploaded.
        /// </summary>
        [JsonProperty("url")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design",
            "CA1056:Uri properties should not be strings",
            Justification = "REST API compatibility")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the viewable name for this file or directory.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the comma-separated lists of file name suffixes or patterns to accept.
        /// </summary>
        [JsonProperty("accept")]
        public string Accept { get; set; }

        /// <summary>
        /// Gets or sets the comma-separated lists of file name suffixes or patterns to reject.
        /// </summary>
        [JsonProperty("reject")]
        public string Reject { get; set; }

        /// <summary>
        /// Gets or sets the maximum recursion depth for folders (0 for infinite).
        /// </summary>
        [JsonProperty("maxRecursionDepth")]
        public int MaxRecursionDepth { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Name}: {this.Url}, depth={this.MaxRecursionDepth}, accept={this.Accept}, reject={this.Reject}";
        } // ToString()
    } // UrlUpload
}
