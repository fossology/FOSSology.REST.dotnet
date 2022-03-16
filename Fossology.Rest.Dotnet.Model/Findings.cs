// ---------------------------------------------------------------------------
// <copyright file="Findings.cs" company="Tethys">
//   Copyright (C) 2022 T. Graf
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
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Information about the licenses of the uploaded files.
    /// </summary>
    public class Findings
    {
        /// <summary>
        /// Gets the short names of the licenses found by the scanner.
        /// </summary>
        [JsonProperty("scanner")]
        public List<string> Scanner { get; }

        /// <summary>
        /// Gets the conclusions, i.e. license(s) decided by user.
        /// </summary>
        [JsonProperty("conclusions")]
        public List<string> Conclusions { get; }

        /// <summary>
        /// Gets the copyrights.
        /// </summary>
        [JsonProperty("copyright")]
        public List<string> Copyrights { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Findings"/> class.
        /// </summary>
        public Findings()
        {
            this.Scanner = new List<string>();
            this.Conclusions = new List<string>();
            this.Copyrights = new List<string>();
        } // UploadLicenses()

        /// <inheritdoc />
        public override string ToString()
        {
            return $"S={Support.ListToString(this.Scanner)}, C={Support.ListToString(this.Conclusions)}: Copy={Support.ListToString(this.Copyrights)}";
        } // ToString()
    } // Findings
}
