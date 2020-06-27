// ---------------------------------------------------------------------------
// <copyright file="ReuseInfo.cs" company="Tethys">
//   Copyright (C) 2019-2020 T. Graf
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
    /// Trigger reuse information.
    /// </summary>
    public class ReuseInfo
    {
        /// <summary>
        /// Gets or sets the id of the upload to reuse.
        /// </summary>
        [JsonProperty("reuse_upload")]
        public int ReuseUploadId { get; set; }

        /// <summary>
        /// Gets or sets the id of the group of the reused upload.
        /// </summary>
        [JsonProperty("reuse_group")]
        public int ReuseGroup { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to reuse the main license.
        /// </summary>
        [JsonProperty("reuse_main")]
        public bool ReuseMain { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to reuse
        /// bulk phrases from reused packages.
        /// </summary>
        [JsonProperty("reuse_enhanced")]
        public bool ReuseEnhanced { get; set; }
    } // ReuseInfo
}
