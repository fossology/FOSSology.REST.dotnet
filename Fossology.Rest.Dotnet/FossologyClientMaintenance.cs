// ---------------------------------------------------------------------------
// <copyright file="FossologyClientMaintenance.cs" company="Tethys">
//   Copyright (C) 2023 T. Graf
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

    /// <summary>
    /// Client for the SW360 REST API.
    /// </summary>
    public partial class FossologyClient
    {
        /// <summary>
        /// Initiates maintenance.
        /// </summary>
        /// <param name="info">The information.</param>
        /// <returns>
        ///   <see cref="Result" />.
        /// </returns>
        public Result InitiateMaintenance(CreateMaintenanceInfo info)
        {
            Log.Debug("Initiating maintenance...");

            var json = JsonConvert.SerializeObject(info);
            var request = new RestRequest(this.Url + "/maintenance", Method.Post);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(json);
            request.AddHeader("Content-Type", "application/json");

            var response = this.api.Execute(request);
            if (response?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            if (result == null)
            {
                Log.Error("Got empty response!");
            }
            else
            {
                if (result.Code == 201)
                {
                    Log.Debug($"Maintenance initiated: {result.Message}");
                }
                else
                {
                    Log.Error($"Error initiating maintenance: {result.Message}");
                } // if
            } // if

            return result;
        } // InitiateMaintenance()
    } // FossologyClient
}
