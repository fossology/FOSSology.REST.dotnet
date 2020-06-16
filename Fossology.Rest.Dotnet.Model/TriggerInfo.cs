// ---------------------------------------------------------------------------
// <copyright file="TriggerInfo.cs" company="Tethys">
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
    /// Fossology job triggering information.
    /// </summary>
    public class TriggerInfo
    {
        /*
         {
          "analysis": {
            "bucket": true,
            "copyright_email_author": true,
            "ecc": true,
            "keyword": true,
            "mime": true,
            "monk": true,
            "nomos": true,
            "ojo": true,
            "package": true
          },
          "decider": {
            "nomos_monk": true,
            "bulk_reused": true,
            "new_scanner": true,
            "ojo_decider": true
          },
          "reuse": {
            "reuse_upload": 0,
            "reuse_group": 0,
            "reuse_main": true,
            "reuse_enhanced": true
          }
        }
         */

        /// <summary>
        /// Trigger decider information.
        /// </summary>
        public class DeciderPart
        {
            /// <summary>
            /// Gets or sets a value indicating whether to use the nomos/monk decisions.
            /// </summary>
            [JsonProperty("nomos_monk")]
            public bool NomosMonk { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether to reuse bulk result.
            /// </summary>
            [JsonProperty("bulk_reused")]
            public bool BulkReused { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether to use the new scanner.
            /// </summary>
            [JsonProperty("new_scanner")]
            public bool NewScanner { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether to use the <c>ojo</c> decider.
            /// </summary>
            [JsonProperty("ojo_decider")]
            public bool OjoDecider { get; set; }
        } // DeciderJobPart

        /// <summary>
        /// Trigger reuse information.
        /// </summary>
        public class ReusePart
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
        } // ReuseJobPart

        /// <summary>
        /// Gets or sets the analysis settings.
        /// </summary>
        [JsonProperty("analysis")]
        public Analysis Analysis { get; set; }

        /// <summary>
        /// Gets or sets the decider settings.
        /// </summary>
        [JsonProperty("decider")]
        public DeciderPart Decider { get; set; }

        /// <summary>
        /// Gets or sets the reuse settings.
        /// </summary>
        [JsonProperty("reuse")]
        public ReusePart Reuse { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerInfo"/> class.
        /// </summary>
        public TriggerInfo()
        {
            this.Analysis = new Analysis();
            this.Decider = new DeciderPart();
            this.Reuse = new ReusePart();
        } // TriggerInfo()
    } // TriggerInfo
}
