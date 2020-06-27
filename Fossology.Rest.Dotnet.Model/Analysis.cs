// ---------------------------------------------------------------------------
// <copyright file="Analysis.cs" company="Tethys">
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
    /// Trigger analysis information.
    /// </summary>
    public class Analysis
    {
        /// <summary>
        /// Gets or sets a value indicating whether to use the bucket.
        /// </summary>
        [JsonProperty("bucket")]
        public bool Bucket { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use copyright email author.
        /// </summary>
        [JsonProperty("copyright_email_author")]
        public bool CopyrightEmailAuthor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the ecc scanner.
        /// </summary>
        [JsonProperty("ecc")]
        public bool Ecc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the keyword scanner.
        /// </summary>
        [JsonProperty("keyword")]
        public bool Keyword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the mime scanner.
        /// </summary>
        [JsonProperty("mime")]
        public bool Mime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the monk scanner.
        /// </summary>
        [JsonProperty("monk")]
        public bool Monk { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the nomos scanner.
        /// </summary>
        [JsonProperty("nomos")]
        public bool Nomos { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the <c>ojo</c> scanner.
        /// </summary>
        [JsonProperty("ojo")]
        public bool Ojo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the package scanner.
        /// </summary>
        [JsonProperty("package")]
        public bool Package { get; set; }
    } // Analysis
}
