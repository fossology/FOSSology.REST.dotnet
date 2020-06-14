#region Header
// ---------------------------------------------------------------------------
// <copyright file="TokenResponse.cs" company="Tethys">
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
#endregion

namespace Fossology.Rest.Dotnet.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// Data returned by a token generation request.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class TokenResponse
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the authorization value.
        /// </summary>
        [JsonProperty("Authorization")]
        public string Authorization { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS        
        /// <summary>
        /// Gets the plain token.
        /// </summary>
        /// <returns>The token.</returns>
        public string GetPlainToken()
        {
            if (string.IsNullOrEmpty(this.Authorization))
            {
                return string.Empty;
            } // if

            var token = this.Authorization.Substring("Bearer ".Length);
            return token;
        } // GetPlainToken()

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Authorization}";
        } // ToString()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        #endregion // PRIVATE METHODS

    } // ServerUpload
}
