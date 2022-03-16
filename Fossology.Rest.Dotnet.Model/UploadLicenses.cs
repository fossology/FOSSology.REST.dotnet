// ---------------------------------------------------------------------------
// <copyright file="UploadLicenses.cs" company="Tethys">
//   Copyright (C) 2020-2022 T. Graf
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
    /// Information about the licenses of the uploaded files.
    /// </summary>
    public class UploadLicenses
    {
        /// <summary>
        /// Gets or sets the relative file path.
        /// </summary>
        [JsonProperty("filePath")]
        public string FilePath { get; set; }

        /// <summary>
        /// Gets the findings for this file.
        /// </summary>
        [JsonProperty("findings")]
        public Findings Findings { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadLicenses"/> class.
        /// </summary>
        public UploadLicenses()
        {
            this.Findings = new Findings();
        } // UploadLicenses()

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.FilePath}: {this.Findings}";
        } // ToString()
    } // UploadLicenses
}
