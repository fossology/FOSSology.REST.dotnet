// ---------------------------------------------------------------------------
// <copyright file="TokenRequest.cs" company="Tethys">
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
    /// Information to create an access token.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class TokenRequest
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the token name.
        /// </summary>
        [JsonProperty("token_name")]
        public string TokenName { get; set; }

        /// <summary>
        /// Gets or sets the token scope, one of (read, write).
        /// </summary>
        [JsonProperty("token_scope")]
        public string TokenScope { get; set; }

        /// <summary>
        /// Gets or sets the token expiry date.
        /// </summary>
        /// <remarks>Date format is yyyy-mm-dd.</remarks>
        [JsonProperty("token_expire")]
        public string TokenExpire { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.TokenName}: {this.Username}, {this.Password}, {this.TokenScope}, {this.TokenExpire}";
        } // ToString()
    } // TokenRequest
}
