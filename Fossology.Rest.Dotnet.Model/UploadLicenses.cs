// ---------------------------------------------------------------------------
// <copyright file="UploadLicenses.cs" company="Tethys">
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
    using System.Collections.Generic;
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
        /// Gets the short names of the found licenses.
        /// </summary>
        [JsonProperty("agentFindings")]
        public List<string> AgentFindings { get; }

        /// <summary>
        /// Gets the conclusions, i.e. license(s) decided by user.
        /// </summary>
        [JsonProperty("conclusions")]
        public List<string> Conclusions { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.FilePath}: C={Support.ListToString(this.Conclusions)}: F={Support.ListToString(this.AgentFindings)}";
        } // ToString()
    } // UploadLicenses
}
