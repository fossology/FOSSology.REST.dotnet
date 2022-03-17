// ---------------------------------------------------------------------------
// <copyright file="RestApi.cs" company="Tethys">
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
    using System;
    using System.IO;
    using System.Net;

    using Fossology.Rest.Dotnet.Model;

    using RestSharp;
    using RestSharp.Serialization.Json;

    /// <summary>
    /// Basic REST API implementation.
    /// </summary>
    public class RestApi
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The client.
        /// </summary>
        private readonly RestClient client;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the access token.
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a before request handler (for extensibility).
        /// </summary>
        public Action<RestRequest> BeforeRequest { get; set; }

        /// <summary>
        /// Gets or sets an after response handler (for extensibility).
        /// </summary>
        public Action<IRestResponse> AfterResponse { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="RestApi" /> class.
        /// </summary>
        /// <param name="hostUrl">The host URL.</param>
        /// <param name="accessToken">The access token.</param>
        public RestApi(string hostUrl, string accessToken)
        {
            this.AccessToken = accessToken;
            this.client = new RestClient(hostUrl /* + ApiNamespace*/);
        } // RestApi()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>Gets the response from the specified URL via GET.</summary>
        /// <param name="url">The URL.</param>
        /// <param name="ignoreResultCode">Ignore the HTTP result code.</param>
        /// <returns>An <see cref="IRestResponse" /> object.</returns>
        public IRestResponse Get(string url, bool ignoreResultCode = false)
        {
            try
            {
                var request = new RestRequest(url, Method.GET);
                this.AddHeaders(request);
                var response = this.client.Execute(request);

                if (!ignoreResultCode)
                {
                    CheckForErrors(response);
                } // if

                return response;
            }
            catch (FossologyApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FossologyApiException(ex);
            } // catch
        } // Get()

        /// <summary>
        /// Gets the response from the specified URL via GET.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// An object of type T.
        /// </returns>
        public T Get<T>(string url)
        {
            try
            {
                var request = new RestRequest(url, Method.GET);
                this.AddHeaders(request);
                var response = this.client.Execute(request);
                CheckForErrors(response);

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (FossologyApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FossologyApiException(ex);
            } // catch
        } // Get()

        /// <summary>
        /// Post the data to the specified URL via POST.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>
        /// An <see cref="IRestResponse" /> object.
        /// </returns>
        public IRestResponse Post(string url, object payload)
        {
            try
            {
                var request = new RestRequest(url, Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.JsonSerializer = new JsonSerializer();
                this.AddHeaders(request);
                request.AddJsonBody(payload);
                var response = this.client.Execute(request);
                CheckForErrors(response);

                return response;
            }
            catch (FossologyApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FossologyApiException(ex);
            } // catch
        } // Post()

        /// <summary>
        /// Post the data to the specified URL via POST.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>
        /// An <see cref="IRestResponse" /> object.
        /// </returns>
        public IRestResponse Post(string url, string payload)
        {
            try
            {
                var request = new RestRequest(url, Method.POST);
                request.RequestFormat = DataFormat.Json;
                this.AddHeaders(request);

                // not working are
                // request.AddJsonBody(payload); // not good
                // request.AddBody(payload); // also not good
                request.AddParameter("Application/Json", payload, ParameterType.RequestBody);
                var response = this.client.Execute(request);
                CheckForErrors(response);

                return response;
            }
            catch (FossologyApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FossologyApiException(ex);
            } // catch
        } // Post()

        /// <summary>
        /// Send data via PATCH.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>An <see cref="IRestResponse" /> object.</returns>
        public IRestResponse Patch(string url, string payload)
        {
            try
            {
                var request = new RestRequest(url, Method.PATCH);
                request.RequestFormat = DataFormat.Json;
                this.AddHeaders(request);

                // not working are
                // request.AddJsonBody(payload); // not good
                // request.AddBody(payload); // also not good
                request.AddParameter("Application/Json", payload, ParameterType.RequestBody);
                var response = this.client.Execute(request);
                CheckForErrors(response);

                return response;
            }
            catch (FossologyApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FossologyApiException(ex);
            } // catch
        } // Patch()

        /// <summary>
        /// Post the data to the specified URL via POST.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        /// An <see cref="IRestResponse" /> object.
        /// </returns>
        public IRestResponse Delete(string url)
        {
            try
            {
                var request = new RestRequest(url, Method.DELETE);
                request.RequestFormat = DataFormat.Json;
                this.AddHeaders(request);
                var response = this.client.Execute(request);
                CheckForErrors(response);

                return response;
            }
            catch (FossologyApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FossologyApiException(ex);
            } // catch
        } // Post()

        /// <summary>
        /// Executes a custom REST request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// An <see cref="IRestResponse" /> object.
        /// </returns>
        public IRestResponse Execute(RestRequest request)
        {
            try
            {
                request.RequestFormat = DataFormat.Json;
                request.JsonSerializer = new JsonSerializer();
                this.AddHeaders(request);
                var response = this.client.Execute(request);
                CheckForErrors(response);

                return response;
            }
            catch (FossologyApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FossologyApiException(ex);
            } // catch
        } // Execute()

        /// <summary>Downloading a file from the given uri.</summary>
        /// <param name="uri">The uri.</param>
        /// <param name="filename">The filename.</param>
        public void DownloadFile(string uri, string filename)
        {
            try
            {
                var request = new RestRequest(uri, Method.GET);
                request.RequestFormat = DataFormat.Json;
                request.JsonSerializer = new JsonSerializer();
                this.AddHeaders(request);
                var data = this.client.DownloadData(request, true);
                System.IO.File.WriteAllBytes(filename, data);
            }
            catch (FossologyApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FossologyApiException(ex);
            } // catch
        } // DownloadFile()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Checks for errors.
        /// </summary>
        /// <param name="response">The response.</param>
        private static void CheckForErrors(IRestResponse response)
        {
            if ((((int)response.StatusCode) == 0)
                || (response.StatusCode == HttpStatusCode.OK)
                || (response.StatusCode == HttpStatusCode.Accepted)
                || (response.StatusCode == HttpStatusCode.Created))
            {
                return;
            } // if

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new FossologyApiException(ErrorCode.Unauthorized, response.StatusCode);
            } // if

            FossologyApiException exception;
            try
            {
                if (string.IsNullOrEmpty(response.Content))
                {
                    exception = new FossologyApiException(
                        ErrorCode.RestApiError, response.StatusCode, string.Empty, null);
                }
                else if (response.Content.ToLower().StartsWith("<html>"))
                {
                    exception = new FossologyApiException(
                        ErrorCode.RestApiError, response.StatusCode, response.Content, null);
                }
                else
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Result>(response.Content);
                    exception = new FossologyApiException(
                        ErrorCode.RestApiError, (HttpStatusCode)result.Code, result.Message, null);
                } // if
            }
            catch
            {
                exception = new FossologyApiException(ErrorCode.RestApiError);
            } // catch

            throw exception;
        } // CheckForErrors()

        /// <summary>
        /// Adds necessary and optional headers.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <example>
        /// <code>Prefer: outlook.timezone="Eastern Standard Time"</code>
        /// </example>
        private void AddHeaders(IRestRequest request)
        {
            request.AddHeader("Authorization", "Bearer " + this.AccessToken);

            if (!string.IsNullOrEmpty(this.Email))
            {
                request.AddHeader("x-AnchorMailbox", this.Email);
            } // if

            if (!string.IsNullOrEmpty(this.TimeZone))
            {
                request.AddHeader("Prefer", $"outlook.timezone=\"{this.TimeZone}\"");
            } // if
        } // AddHeaders()
        #endregion // PRIVATE METHODS
    } // RestApi
}
