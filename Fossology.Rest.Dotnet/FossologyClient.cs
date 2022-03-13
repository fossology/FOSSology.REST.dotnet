// ---------------------------------------------------------------------------
// <copyright file="FossologyClient.cs" company="Tethys">
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

    using Tethys.Logging;

    using JsonSerializer = RestSharp.Serialization.Json.JsonSerializer;

    /*************************************************************************
     * Required NuGet Packages
     * ------------------------
     * - Tethys.Logging 1.3.0, Apache-2.0
     * - Newtonsoft.Json 12.0.3, MIT
     * - RestSharp 106.6.10
     *
     ************************************************************************/

    /// <summary>
    /// Client for the SW360 REST API.
    /// </summary>
    public partial class FossologyClient
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The logger for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(FossologyClient));
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the URL.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Gets the token.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// The API.
        /// </summary>
        private readonly RestApi api;
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="FossologyClient" /> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="token">The token.</param>
        public FossologyClient(string url, string token)
        {
            this.Url = url;
            this.Token = token;
            this.api = new RestApi(this.Url, this.Token);
        } // FossologyClient()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Gets the Fossology version.
        /// </summary>
        /// <returns>A <see cref="VersionInfo"/> object.</returns>
        /// <returns>
        /// Requires no authorization.
        /// </returns>
        public VersionInfo GetVersion()
        {
            Log.Debug("Getting version info...");

            var result = this.api.Get(this.Url + "/version");
            var version = JsonConvert.DeserializeObject<VersionInfo>(result.Content);
            return version;
        } // GetVersion()

        /// <summary>
        /// Gets a token.
        /// </summary>
        /// <param name="requestDetails">The request details.</param>
        /// <returns>The token.</returns>
        public string GetToken(TokenRequest requestDetails)
        {
            Log.Debug($"Requesting token {requestDetails.TokenName} for user {requestDetails.Username}...");

            var json = JsonConvert.SerializeObject(requestDetails);
            var request = new RestRequest(this.Url + "/tokens", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();

            request.AddJsonBody(json);

            var response = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<TokenResponse>(response.Content);

            return result.GetPlainToken();
        } // GetToken()
        #endregion // PUBLIC METHODS
    } // FossologyClient
}
