// ---------------------------------------------------------------------------
// <copyright file="UploadInformationVcs.cs" company="Tethys">
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
    /// Fossology version information.
    /// </summary>
    /// <remarks>
    /// <code>
    /// {
    ///   "location" : {
    ///     "vcsType": "string",
    ///     "vcsUrl": "string",
    ///     "vcsBranch": string,
    ///     "vcsName": "string",
    ///     "vcsUsername": "string",
    ///     "vcsPassword": "string",
    ///   }
    /// }
    /// </code>
    /// </remarks>
    public class UploadInformationVcs
    {
        /// <summary>
        /// Gets or sets the upload location.
        /// </summary>
        [JsonProperty("location")]
        public VcsUpload Location { get; set; }

        /// <summary>
        /// Gets or sets the scan options.
        /// </summary>
        [JsonProperty("scanOptions")]
        public TriggerInfo ScanOptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadInformationVcs" /> class.
        /// </summary>
        /// <param name="scanOptions">The scan options.</param>
        public UploadInformationVcs(TriggerInfo scanOptions = null)
        {
            this.Location = new VcsUpload();
            this.ScanOptions = scanOptions;
        } // UploadInformationVcs()
    } // UploadInformationVcs
}
