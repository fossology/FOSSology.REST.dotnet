#region Header
// ---------------------------------------------------------------------------
// <copyright file="Support.cs" company="Tethys">
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
    using System.Collections.Generic;

    /// <summary>
    /// Support methods.
    /// </summary>
    public class Support
    {
        /// <summary>
        /// List to string.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>A single string.</returns>
        public static string ListToString(IReadOnlyList<string> list)
        {
            if (list == null)
            {
                return string.Empty;
            } // if

            var joined = string.Join(",", list);
            return joined;
        } // ListToString()
    } // Support
}
