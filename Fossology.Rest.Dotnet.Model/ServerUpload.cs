// ---------------------------------------------------------------------------
// <copyright file="ServerUpload.cs" company="Tethys">
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
    /// Information to create an upload from a server.
    /// </summary>
    public class ServerUpload
    {
        /// <summary>
        /// Gets or sets the file path to be uploaded (recursive, supports *).
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the viewable name for this file or directory.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Name}: {this.Path}";
        } // ToString()
    } // ServerUpload
}
