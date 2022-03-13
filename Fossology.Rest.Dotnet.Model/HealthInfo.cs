// ---------------------------------------------------------------------------
// <copyright file="HealthInfo.cs" company="Tethys">
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

namespace Fossology.Rest.Dotnet.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// Fossology health information.
    /// </summary>
    public class HealthInfo
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the scheduler.
        /// </summary>
        [JsonProperty("scheduler")]
        public SchedulerInfo Scheduler { get; set; }

        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        [JsonProperty("db")]
        public DatabaseInfo Database { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Status}: scheduler={this.Scheduler.Status}, database={this.Database.Status}";
        }
    } // HealthInfo
}
