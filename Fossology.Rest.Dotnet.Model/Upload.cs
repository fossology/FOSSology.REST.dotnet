#region Header
// ---------------------------------------------------------------------------
// <copyright file="Upload.cs" company="Tethys">
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
#endregion

namespace Fossology.Rest.Dotnet.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// Fossology version information.
    /// </summary>
    /// <remarks>
    /// <code>
    /// {
    ///   "folderid": 0,
    ///   "foldername": "string",
    ///   "id": 0,
    ///   "description": "string",
    ///   "uploadname": "string",
    ///   "uploaddate": "string",
    ///   "filesize": 0
    /// }
    /// </code>
    /// </remarks>
    public class Upload
    {
        /// <summary>
        /// Gets or sets the folder id.
        /// </summary>
        [JsonProperty("folderid")]
        public int FolderId { get; set; }

        /// <summary>
        /// Gets or sets the folder name.
        /// </summary>
        [JsonProperty("foldername")]
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the upload name.
        /// </summary>
        [JsonProperty("uploadname")]
        public string UploadName { get; set; }

        /// <summary>
        /// Gets or sets the upload date.
        /// </summary>
        [JsonProperty("uploaddate")]
        public string UploadDate { get; set; }

        /// <summary>
        /// Gets or sets the file size.
        /// </summary>
        [JsonProperty("filesize")]
        public int Filesize { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Id}: {this.UploadName}, folder={this.FolderId}, size={this.Filesize}, {this.UploadDate} '{this.Description}'";
        }
    } // Upload
}
