// ---------------------------------------------------------------------------
// <copyright file="UploadSummary.cs" company="Tethys">
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
    /// Information about an upload.
    /// </summary>
    public class UploadSummary
    {
        /// <summary>
        /// Gets or sets the identifier of the upload.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the upload.
        /// </summary>
        [JsonProperty("uploadName")]
        public string UploadName { get; set; }

        /// <summary>
        /// Gets or sets the main license.
        /// </summary>
        [JsonProperty("mainLicense")]
        public string MainLicense { get; set; }

        /// <summary>
        /// Gets or sets the number of unique licenses found.
        /// </summary>
        [JsonProperty("uniqueLicenses")]
        public int UniqueLicenses { get; set; }

        /// <summary>
        /// Gets or sets the number of total licenses found.
        /// </summary>
        [JsonProperty("totalLicenses")]
        public int TotalLicenses { get; set; }

        /// <summary>
        /// Gets or sets the number of unique concluded licenses found.
        /// </summary>
        [JsonProperty("uniqueConcludedLicenses")]
        public int UniqueConcludedLicenses { get; set; }

        /// <summary>
        /// Gets or sets the number of total concluded licenses found.
        /// </summary>
        [JsonProperty("totalConcludedLicenses")]
        public int TotalConcludedLicenses { get; set; }

        /// <summary>
        /// Gets or sets the number of files without clearing decisions.
        /// </summary>
        [JsonProperty("filesToBeCleared")]
        public int FilesToBeCleared { get; set; }

        /// <summary>
        /// Gets or sets the number of files with clearing decisions.
        /// </summary>
        [JsonProperty("filesCleared")]
        public int FilesCleared { get; set; }

        /// <summary>
        /// Gets or sets the clearing status, one of (Open, InProgress, Closed, Rejected).
        /// </summary>
        [JsonProperty("clearingStatus")]
        public string ClearingStatus { get; set; }

        /// <summary>
        /// Gets or sets the number of copyrights found in the upload.
        /// </summary>
        [JsonProperty("copyrightCount")]
        public int CopyrightCount { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Id}, {this.UploadName}: {this.ClearingStatus}: {this.MainLicense}, "
                + $"{this.UniqueLicenses}, {this.TotalLicenses}";
        } // ToString()
    } // UploadSummary
}
