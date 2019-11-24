#region Header
// ---------------------------------------------------------------------------
// <copyright file="TriggerInfo.cs" company="Tethys">
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
    /// Fossology job information.
    /// </summary>
    public class Job
    {
        /*
         {
          "id": 0,
          "name": "string",
          "queueDate": "string",
          "uploadId": 0,
          "userId": 0,
          "groupId": 0,
          "eta": 0,
          "status": "Completed"
        }   
         */

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
        /// Gets or sets the date when the job was queued.
        /// </summary>
        [JsonProperty("queueDate")]
        public string QueueDate { get; set; }

        /// <summary>
        /// Gets or sets the upload id.
        /// </summary>
        [JsonProperty("uploadId")]
        public int UploadId { get; set; }

        /// <summary>
        /// Gets or sets the id of the user who scheduled the job.
        /// </summary>
        [JsonProperty("userId")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Group under which the job was scheduled.
        /// </summary>
        [JsonProperty("groupId")]
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the ETA of job to finish in seconds.
        /// </summary>
        [JsonProperty("eta")]
        public int Eta { get; set; }

        /// <summary>
        /// Gets or sets the current status of the job in the queue.
        /// Can be one of Completed, Failed, Queued, Processing.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Id}: {this.Name}, upload={this.UploadId}, eta={this.Eta}: '{this.Status}'";
        } // ToString()
    } // Job
}
