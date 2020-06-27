// ---------------------------------------------------------------------------
// <copyright file="VcsUpload.cs" company="Tethys">
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
    using Newtonsoft.Json;

    /// <summary>
    /// Information to create an upload from a version control system.
    /// </summary>
    public class VcsUpload
    {
        /// <summary>
        /// Gets or sets the VCS type (svn or git).
        /// </summary>
        [JsonProperty("vcsType")]
        public string VcsType { get; set; }

        /// <summary>
        /// Gets or sets the URL of the repository.
        /// </summary>
        [JsonProperty("vcsUrl")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design",
            "CA1056:Uri properties should not be strings",
            Justification = "REST API compatibility")]
        public string VcsUrl { get; set; }

        /// <summary>
        /// Gets or sets the branch to checkout for analysis (for Git only).
        /// </summary>
        [JsonProperty("vcsBranch")]
        public string VcsBranch { get; set; }

        /// <summary>
        /// Gets or sets the display name of the upload.
        /// </summary>
        [JsonProperty("vcsName")]
        public string VcsName { get; set; }

        /// <summary>
        /// Gets or sets the VCS username.
        /// </summary>
        [JsonProperty("vcsUsername")]
        public string VcsUsername { get; set; }

        /// <summary>
        /// Gets or sets the VCS password.
        /// </summary>
        [JsonProperty("vcsPassword")]
        public string VcsPassword { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.VcsName}: {this.VcsType}, {this.VcsUrl}, ({this.VcsBranch})";
        } // ToString()
    } // VcsUpload
}
