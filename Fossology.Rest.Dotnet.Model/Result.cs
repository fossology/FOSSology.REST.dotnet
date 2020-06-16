// ---------------------------------------------------------------------------
// <copyright file="Result.cs" company="Tethys">
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
    public class Result
    {
        /*
         [
          {
            "code": 200,
            "message": "string",
            "type": "INFO"
          }
        ]
         */

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Code}: {this.Type}: '{this.Message}'";
        }
    } // Result
}
