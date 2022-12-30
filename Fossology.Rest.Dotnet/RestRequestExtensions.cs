// ---------------------------------------------------------------------------
// <copyright file="RestRequestExtensions.cs" company="Tethys">
//   Copyright (C) 2022 T. Graf
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
    using RestSharp;

    /// <summary>
    /// Extension methods for RestRequest.
    /// </summary>
    internal static class RestRequestExtensions
    {
        /// <summary>
        /// Adds a file.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <param name="f">The f.</param>
        /// <param name="uploadFinished">The optional upload finished callback.</param>
        /// <param name="uploadProgress">The optional upload progress callback.</param>
        /// <returns>
        /// A <see cref="RestRequest" />.
        /// </returns>
        public static RestRequest AddFile(
            this RestRequest req,
            FileParameter f,
            Action uploadFinished = null,
            Action<float> uploadProgress = null)
        {
            return req.AddFile(f.Name, f.GetFile, f.FileName, f.ContentType);
        } // AddFile()
    } // RestRequestExtensions
}
