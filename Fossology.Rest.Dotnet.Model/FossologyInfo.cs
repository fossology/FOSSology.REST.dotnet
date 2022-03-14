// ---------------------------------------------------------------------------
// <copyright file="FossologyInfo.cs" company="Tethys">
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
    using Newtonsoft.Json;

    /// <summary>
    /// Fossology information.
    /// </summary>
    public class FossologyInfo
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the branch name.
        /// </summary>
        [JsonProperty("branchName")]
        public string BranchName { get; set; }

        /// <summary>
        /// Gets or sets the commit hash.
        /// </summary>
        [JsonProperty("commitHash")]
        public string CommitHash { get; set; }

        /// <summary>
        /// Gets or sets the commit date.
        /// </summary>
        [JsonProperty("commitDate")]
        public string CommitDate { get; set; }

        /// <summary>
        /// Gets or sets the build date.
        /// </summary>
        [JsonProperty("buildDate")]
        public string BuildDate { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Version}, {this.BranchName}, parent={this.CommitHash}, {this.CommitDate}, {this.BuildDate}";
        }
    } // FossologyInfo
}
