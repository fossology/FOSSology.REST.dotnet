// ---------------------------------------------------------------------------
// <copyright file="FossologyClientUser.cs" company="Tethys">
//   Copyright (C) 2019-2022 T. Graf
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
    using System.Collections.Generic;
    using Fossology.Rest.Dotnet.Model;

    using Newtonsoft.Json;

    /// <summary>
    /// Client for the SW360 REST API.
    /// </summary>
    public partial class FossologyClient
    {
        /// <summary>
        /// Gets a list of all users.
        /// </summary>
        /// <returns>A <see cref="User"/> object.</returns>
        public IReadOnlyList<User> GetUserList()
        {
            Log.Debug("Getting users...");

            var result = this.api.Get(this.Url + "/users");
            var list = JsonConvert.DeserializeObject<List<User>>(
                result.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    });
            return list;
        } // GetUser()

        /// <summary>
        /// Gets the user with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="User"/> object.</returns>
        public User GetUser(int id)
        {
            Log.Debug($"Getting user {id}...");

            var result = this.api.Get(this.Url + $"/users/{id}");
            var user = JsonConvert.DeserializeObject<User>(result.Content);
            return user;
        } // GetUser()

        /// <summary>
        /// Deletes the user with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        public Result DeleteUser(int id)
        {
            Log.Debug($"Deleting user {id}...");

            var response = this.api.Delete(this.Url + $"/users/{id}");
            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            return result;
        } // DeleteUser()
    } // FossologyClient
}
