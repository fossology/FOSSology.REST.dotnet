﻿// ---------------------------------------------------------------------------
// <copyright file="FossologyClientJob.cs" company="Tethys">
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

    using RestSharp;

    using JsonSerializer = RestSharp.Serialization.Json.JsonSerializer;

    /// <summary>
    /// Client for the SW360 REST API.
    /// </summary>
    public partial class FossologyClient
    {
        /// <summary>Triggers a job.</summary>
        /// <param name="folderId">The folder identifier.</param>
        /// <param name="uploadId">The upload identifier.</param>
        /// <param name="details">The details.</param>
        /// <param name="groupName">The group name to chose while scheduling jobs.</param>
        /// <returns>An <see cref="Result" /> object.</returns>
        /// <remarks>The message property of the result contains the job id
        /// which is needed for further operations.</remarks>
        public Result TriggerJob(int folderId, int uploadId, TriggerInfo details, string groupName = "")
        {
            Log.Debug($"Triggering job for upload {uploadId}, folder={folderId}...");

            var json = JsonConvert.SerializeObject(details);
            var request = new RestRequest(this.Url + "/jobs", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            request.AddHeader("uploadId", uploadId.ToString());
            request.AddHeader("folderId", folderId.ToString());
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            request.AddJsonBody(json);

            var response = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            Log.Debug($"TriggerInfo {result.Message} triggered.");

            return result;
        } // TriggerJob()

        /// <summary>
        /// Gets the job with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Job"/> object.</returns>
        public Job GetJob(int id)
        {
            Log.Debug($"Getting job {id}...");

            var result = this.api.Get(this.Url + $"/jobs/{id}");
            var job = JsonConvert.DeserializeObject<Job>(
                result.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    });
            return job;
        } // GetJob()

        /// <summary>
        /// Gets a list of all jobs.
        /// </summary>
        /// <returns>A <see cref="Job"/> object.</returns>
        public IReadOnlyList<Job> GetJobList()
        {
            Log.Debug("Getting all jobs...");

            var result = this.api.Get(this.Url + "/jobs");
            var list = JsonConvert.DeserializeObject<List<Job>>(
                result.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    });
            return list;
        } // GetJobList()
    } // FossologyClient
}