// ---------------------------------------------------------------------------
// <copyright file="CopyrightEntry.cs" company="Tethys">
//   Copyright (C) 2023 T. Graf
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
    /// Information about a single copyright.
    /// </summary>
    public class CopyrightEntry
    {
        /// <summary>
        /// Gets or sets the copyright statement.
        /// </summary>
        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        /// <summary>
        /// Gets or sets the list of files where the copyright was found.
        /// </summary>
        [JsonProperty("filePath")]
        public List<string> FilePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyrightEntry"/> class.
        /// </summary>
        public CopyrightEntry()
        {
            this.FilePath = new List<string>();
        } // CopyrightEntry()

        /// <inheritdoc />
        public override string ToString()
        {
            return $"#{this.FilePath.Count}: {this.Copyright}";
        } // ToString()
    } // CopyrightEntry
}
