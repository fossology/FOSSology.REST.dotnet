// ---------------------------------------------------------------------------
// <copyright file="UploadInformationUrl.cs" company="Tethys">
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
    ///     "url": "string",
    ///     "name": "string",
    ///     "accept": "string",
    ///     "reject": "string",
    ///     "maxRecursionDepth": 0
    /// }
    /// </code>
    /// </remarks>
    public class UploadInformationUrl
    {
        /// <summary>
        /// Gets or sets the upload location.
        /// </summary>
        [JsonProperty("location")]
        public UrlUpload Location { get; set; }

        /// <summary>
        /// Gets or sets the scan options.
        /// </summary>
        [JsonProperty("scanOptions")]
        public TriggerInfo ScanOptions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadInformationUrl" /> class.
        /// </summary>
        /// <param name="scanOptions">The scan options.</param>
        public UploadInformationUrl(TriggerInfo scanOptions = null)
        {
            this.Location = new UrlUpload();
            this.ScanOptions = scanOptions;
        } // UploadInformationUrl()
    } // UploadInformation
}
