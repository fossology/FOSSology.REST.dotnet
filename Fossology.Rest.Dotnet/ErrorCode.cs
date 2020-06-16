// ---------------------------------------------------------------------------
// <copyright file="ErrorCode.cs" company="Tethys">
//   Copyright (C) 2019-2020 T. Graf
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

namespace Fossology.Rest.Dotnet
{
    /// <summary>
    /// Enumeration of error codes.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// No error.
        /// </summary>
        NoError = 0,

        /// <summary>
        /// An unknown error.
        /// </summary>
        Unknown = 1,

        /// <summary>
        /// User or client not authorized.
        /// </summary>
        Unauthorized = 2,

        /// <summary>
        /// A REST API error has happened.
        /// </summary>
        RestApiError = 3,
    } // ErrorCode
}
