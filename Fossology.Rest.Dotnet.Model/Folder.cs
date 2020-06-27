// ---------------------------------------------------------------------------
// <copyright file="Folder.cs" company="Tethys">
//   Copyright (C) 2019 T. Graf
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
    public class Folder
    {
        /*
         [
          {
            "id": 0,
            "name": "string",
            "description": "string",
            "parent": 0
          }
        ]
         */

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the parent folder.
        /// </summary>
        [JsonProperty("parent")]
        public int Parent { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Id}: {this.Name}, parent={this.Parent}, '{this.Description}'";
        }
    } // Folder
}
