// ---------------------------------------------------------------------------
// <copyright file="FossologyClientReport.cs" company="Tethys">
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
    using Fossology.Rest.Dotnet.Model;

    using Newtonsoft.Json;

    using RestSharp;

    using JsonSerializer = RestSharp.Serialization.Json.JsonSerializer;

    /// <summary>
    /// Client for the SW360 REST API.
    /// </summary>
    public partial class FossologyClient
    {
        /// <summary>
        /// Triggers the report generation.
        /// </summary>
        /// <param name="uploadId">The upload identifier.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <param name="groupName">The group name to chose while deleting the package.</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        /// <remarks>
        /// The message property of the result contains the report id
        /// which is needed for further operations.
        /// </remarks>>
        public Result TriggerReportGeneration(int uploadId, string reportFormat, string groupName = "")
        {
            Log.Debug($"Triggering report generation for upload {uploadId}, format={reportFormat}...");

            var request = new RestRequest(this.Url + "/report", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.AddHeader("uploadId", uploadId.ToString());
            request.AddHeader("reportFormat", reportFormat);
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            Log.Debug($"Report {result.Message}: generation started.");

            return result;
        } // TriggerReportGeneration()

        /// <summary>
        /// Downloads the specified report.
        /// </summary>
        /// <param name="reportId">The report identifier.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="groupName">The group name to chose while deleting the package.</param>
        /// <returns><c>true</c> if the file has been successfully downloaded; otherwise <c>false</c>.</returns>
        public bool DownloadReport(int reportId, string fileName, string groupName = "")
        {
            Log.Debug($"Getting report {reportId}...");

            var request = new RestRequest(this.Url + $"/report/{reportId}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            this.api.Execute(request);

            // There could be an error response, in this case an exception would get thrown
            // If there is no exception, then we can normally download the file.
            this.api.DownloadFile(this.Url + $"/report/{reportId}", fileName);
            return true;
        } // DownloadReport()
    } // FossologyClient
}
