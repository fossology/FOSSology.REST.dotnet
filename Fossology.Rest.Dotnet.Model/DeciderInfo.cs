// ---------------------------------------------------------------------------
// <copyright file="DeciderInfo.cs" company="Tethys">
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
    /// Trigger decider information.
    /// </summary>
    public class DeciderInfo
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

        /// <summary>
        /// Gets or sets the type of the auto conclude license.
        /// Auto conclude license if they are of provided type. Omit the field or keep as
        /// empty string to not use this option.
        /// </summary>
        [JsonProperty("conclude_license_type")]
        public string ConcludeLicenseType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to do copyright deactivation.
        /// </summary>
        [JsonProperty("copyright_deactivation")]
        public bool CopyrightDeactivation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use copyright clutter removal.
        /// </summary>
        [JsonProperty("copyright_clutter_removal")]
        public bool CopyrightClutterRemoval { get; set; }
    } // DeciderInfo
}
