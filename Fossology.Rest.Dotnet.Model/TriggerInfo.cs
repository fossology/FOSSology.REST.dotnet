// ---------------------------------------------------------------------------
// <copyright file="TriggerInfo.cs" company="Tethys">
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
          },
          "scancode": {
            "license": true,
            "copyright": true,
            "email": true,
            "url": true
          }
        }
         */

        /// <summary>
        /// Gets or sets the analysis settings.
        /// </summary>
        [JsonProperty("analysis")]
        public Analysis Analysis { get; set; }

        /// <summary>
        /// Gets or sets the decider settings.
        /// </summary>
        [JsonProperty("decider")]
        public DeciderInfo Decider { get; set; }

        /// <summary>
        /// Gets or sets the reuse settings.
        /// </summary>
        [JsonProperty("reuse")]
        public ReuseInfo Reuse { get; set; }

        /// <summary>
        /// Gets or sets the ScanCode settings.
        /// </summary>
        [JsonProperty("scancode")]
        public ScanCodeInfo Scancode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerInfo"/> class.
        /// </summary>
        public TriggerInfo()
        {
            this.Analysis = new Analysis();
            this.Decider = new DeciderInfo();
            this.Reuse = new ReuseInfo();
            this.Scancode = new ScanCodeInfo();
        } // TriggerInfo()
    } // TriggerInfo
}
