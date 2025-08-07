// ---------------------------------------------------------------------------
// <copyright file="Upload.cs" company="Tethys">
//   Copyright (C) 2019-2025 T. Graf
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
        /// Gets or sets the assignee id of the upload.
        /// </summary>
        [JsonProperty("assignee", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Assignee { get; set; }

        /// <summary>
        /// Gets or sets the date, when a user was assigned to the upload.
        /// </summary>
        [JsonProperty("assigneeDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AssigneeDate { get; set; }

        /// <summary>
        /// Gets or sets the date, when the upload was closed or rejected.
        /// </summary>
        [JsonProperty("closingDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ClosingDate { get; set; }

        /// <summary>
        /// Gets or sets the hash of the file.
        /// </summary>
        [JsonProperty("hash")]
        public Hash Hash { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Id}: {this.UploadName}, folder={this.FolderId}, size={this.Hash.Size}, {this.UploadDate} '{this.Description}'";
        }
    } // Upload
}
