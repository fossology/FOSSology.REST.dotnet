#region Header
// ---------------------------------------------------------------------------
// <copyright file="User.cs" company="Tethys">
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
    using Newtonsoft.Json;

    /// <summary>
    /// Fossology user information.
    /// </summary>
    public class User
    {
        /*
        {
          "id": 0,
          "name": "string",
          "description": "string",
          "email": "string",
          "accessLevel": "none",
          "rootFolderId": 0,
          "emailNotification": true,
          "agents": {
            "bucket": true,
            "copyright_email_author": true,
            "ecc": true,
            "keyword": true,
            "mime": true,
            "monk": true,
            "nomos": true,
            "ojo": true,
            "package": true
          }
        }    
         */

        /// <summary>
        /// Fossology user agent information.
        /// </summary>
        public class UserAgents
        {
            /// <summary>
            /// Gets or sets a value indicating whether bucket analysis should
            /// be run on this upload
            /// </summary>
            [JsonProperty("bucket")]
            public bool Bucket { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether copyright/Email/URL/Author
            /// analysis should be run on this upload.
            /// </summary>
            [JsonProperty("copyright_email_author")]
            public bool CopyrightEmailAuthor { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether ECC analysis should be run on this upload.
            /// </summary>
            [JsonProperty("ecc")]
            public bool Ecc { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether keyword analysis should be run on this upload.
            /// </summary>
            [JsonProperty("keyword")]
            public bool Keyword { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether MIME analysis should be run on this upload.
            /// </summary>
            [JsonProperty("mime")]
            public bool Mime { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether monk analysis should be run on this upload.
            /// </summary>
            [JsonProperty("monk")]
            public bool Monk { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether nomos analysis should be run on this upload.
            /// </summary>
            [JsonProperty("nomos")]
            public bool Nomos { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether <c>ojo</c> analysis should be run on this upload.
            /// </summary>
            [JsonProperty("ojo")]
            public bool Ojo { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether package analysis should be run on this upload.
            /// </summary>
            [JsonProperty("package")]
            public bool Package { get; set; }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the access level.
        /// </summary>
        [JsonProperty("accessLevel")]
        public string AccessLevel { get; set; }

        /// <summary>
        /// Gets or sets the root folder id.
        /// </summary>
        [JsonProperty("rootFolderId")]
        public int RootFolderId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user should receive email notifications.
        /// </summary>
        [JsonProperty("emailNotification")]
        public string EmailNotification { get; set; }

        /// <summary>
        /// Gets or sets the agent settings.
        /// </summary>
        [JsonProperty("agents")]
        public UserAgents Agents { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Id}: {this.Name}, '{this.Description}'";
        } // ToString()
    } // User
}
