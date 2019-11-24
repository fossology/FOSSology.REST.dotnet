#region Header
// ---------------------------------------------------------------------------
// <copyright file="VersionInfo.cs" company="Tethys">
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
#endregion

namespace Fossology.Rest.Dotnet.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Fossology version information.
    /// </summary>
    [DataContract]
    public class VersionInfo
    {
        /*
         {
            "version": "string",
            "security": [
                "string"
            ]
         }
         */

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [DataMember(Name = "version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the supported authentication methods.
        /// </summary>
        [DataMember(Name = "security")]
        public List<string> Security { get; set; }
    }
}
