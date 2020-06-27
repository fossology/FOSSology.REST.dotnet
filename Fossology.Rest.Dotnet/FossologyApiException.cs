// ---------------------------------------------------------------------------
// <copyright file="FossologyApiException.cs" company="Tethys">
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
    using System;
    using System.Net;

    /// <summary>
    /// Exception used with in Fossology.Rest.Dotnet.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class FossologyApiException : Exception
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the error code.
        /// </summary>
        public ErrorCode ErrorCode { get; }

        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="FossologyApiException"/> class.
        /// </summary>
        public FossologyApiException()
        {
            this.ErrorCode = ErrorCode.Unknown;
            this.HttpStatusCode = HttpStatusCode.OK;
        } // FossologyApiException()

        /// <summary>
        /// Initializes a new instance of the <see cref="FossologyApiException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FossologyApiException(string message)
            : base(message)
        {
            this.ErrorCode = ErrorCode.Unknown;
            this.HttpStatusCode = HttpStatusCode.Unused;
        } // FossologyApiException()

        /// <summary>
        /// Initializes a new instance of the <see cref="FossologyApiException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public FossologyApiException(ErrorCode errorCode)
        {
            this.ErrorCode = errorCode;
            this.HttpStatusCode = HttpStatusCode.Unused;
        } // FossologyApiException()

        /// <summary>
        /// Initializes a new instance of the <see cref="FossologyApiException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        public FossologyApiException(ErrorCode errorCode, string message)
            : base(message)
        {
            this.ErrorCode = errorCode;
            this.HttpStatusCode = HttpStatusCode.Unused;
        } // FossologyApiException()

        /// <summary>
        /// Initializes a new instance of the <see cref="FossologyApiException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        public FossologyApiException(ErrorCode errorCode, HttpStatusCode httpStatusCode)
            : base(string.Empty)
        {
            this.ErrorCode = errorCode;
            this.HttpStatusCode = httpStatusCode;
        } // FossologyApiException()

        /// <summary>
        /// Initializes a new instance of the <see cref="FossologyApiException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public FossologyApiException(
            ErrorCode errorCode,
            HttpStatusCode httpStatusCode,
            string message,
            Exception innerException)
            : base(message, innerException)
        {
            this.ErrorCode = errorCode;
            this.HttpStatusCode = httpStatusCode;
        } // FossologyApiException()

        /// <summary>
        /// Initializes a new instance of the <see cref="FossologyApiException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public FossologyApiException(Exception innerException)
            : base(string.Empty, innerException)
        {
            this.ErrorCode = ErrorCode.Unknown;
            this.HttpStatusCode = HttpStatusCode.Unused;
        } // FossologyApiException()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (this.HttpStatusCode == HttpStatusCode.Unused)
            {
                return $"{this.ErrorCode}, {this.Message}";
            } // if

            return $"{this.ErrorCode}, {this.HttpStatusCode}, {this.Message}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // FossologyApiException()
}
