// ---------------------------------------------------------------------------
// <copyright file="SearchResult.cs" company="Tethys">
//   Copyright (C) 2019 T. Graf
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
    /// Fossology version information.
    /// </summary>
    /// <remarks>
    /// <code>
    /// {
    ///   "upload": {
    ///     "folderid": 0,
    ///     "foldername": "string",
    ///     "id": 0,
    ///     "description": "string",
    ///     "uploadname": "string",
    ///     "uploaddate": "string",
    ///     "filesize": 0
    ///   },
    ///   "uploadTreeId": 0,
    ///   "filename": "string"
    /// }
    /// </code>
    /// </remarks>
    public class SearchResult
    {
        /// <summary>
        /// Gets or sets the upload.
        /// </summary>
        [JsonProperty("upload")]
        public Upload Upload { get; set; }

        /// <summary>
        /// Gets or sets the upload tree id.
        /// </summary>
        [JsonProperty("uploadTreeId")]
        public int UploadTreeId { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        [JsonProperty("filename")]
        public string Filename { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Upload.Id}: {this.Upload.UploadName}, TreeId={this.UploadTreeId}";
        }
    } // SearchResult
}
