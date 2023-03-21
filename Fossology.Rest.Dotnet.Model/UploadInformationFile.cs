// ---------------------------------------------------------------------------
// <copyright file="UploadInformationFile.cs" company="Tethys">
//   Copyright (C) 2019-2023 T. Graf
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
    /// Fossology upload information for files.
    /// </summary>
    public class UploadInformationFile
    {
        // IMPORTANT: not 'location' information required at the moment

        /// <summary>
        /// Gets or sets the scan options.
        /// </summary>
        [JsonProperty("scanOptions")]
        public TriggerInfo ScanOptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadInformationFile" /> class.
        /// </summary>
        public UploadInformationFile()
        {
            this.ScanOptions = new TriggerInfo();
        } // UploadInformationFile()
    } // UploadInformationFile
}
