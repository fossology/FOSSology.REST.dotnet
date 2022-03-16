// ---------------------------------------------------------------------------
// <copyright file="FossologyClientUpload.cs" company="Tethys">
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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
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
        /// Uploads the package.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="folderId">The folder identifier.</param>
        /// <param name="groupName">The group name to chose while uploading the package.</param>
        /// <param name="uploadFinished">The optional upload finished callback.</param>
        /// <param name="uploadProgress">The optional upload progress callback.</param>
        /// <param name="description">The description.</param>
        /// <param name="accessLevel">The access level.</param>
        /// <param name="ignoreScm">if set to <c>true</c> ignore SCM files.</param>
        /// <param name="applyGlobal">if set to <c>true</c> apply global decisions.</param>
        /// <returns>An <see cref="Result" /> object.</returns>
        /// <remarks>The message property of the result contains the upload id
        /// which is needed for further operations.</remarks>
        public Result UploadPackage(
            string fileName,
            int folderId,
            string groupName = "",
            Action uploadFinished = null,
            Action<float> uploadProgress = null,
            string description = "",
            string accessLevel = "public",
            bool ignoreScm = true,
            bool applyGlobal = false)
        {
            Log.Debug($"Uploading package {fileName} to folder {folderId}...");

            if (!File.Exists(fileName))
            {
                throw new FossologyApiException(ErrorCode.FileNotFound, fileName);
            } // if

            var request = new RestRequest(this.Url + "/uploads", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("folderId", folderId.ToString());
            request.AddHeader("groupName", groupName);
            request.AddHeader("uploadDescription", description);
            request.AddHeader("public", accessLevel);
            request.AddHeader("ignoreScm", ignoreScm.ToString());
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("applyGlobal", applyGlobal.ToString());

            var fi = new FileInfo(fileName);
            var item = new FileParameter
                                  {
                                      Name = "fileInput",
                                      ContentLength = fi.Length,
                                      FileName = fi.Name,
                                      ContentType = "application/octet-stream",
                                  };
            item.Writer = stream =>
            {
                var bufferSize = 1024 * 1024; // 1 MB
                var sofar = 0;

                var buffer = new byte[bufferSize];
                using (var fs = new FileStream(fileName, FileMode.Open))
                {
                    while (sofar < fi.Length)
                    {
                        var read = fs.Read(buffer, 0, bufferSize);

                        if (read < bufferSize)
                        {
                            bufferSize = read;
                        } // if

                        stream.Write(buffer, 0, bufferSize);
                        sofar += bufferSize;

                        var percentage = sofar * 100 / fi.Length;
                        var progress = percentage / 100f;

                        Log.Debug($"Upload progress = {progress}");
                        uploadProgress?.Invoke(progress);
                    } // while
                } // using

                uploadFinished?.Invoke();
            };
            request.Files.Add(item);

            request.AlwaysMultipartFormData = true;
            var resultRaw = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            Log.Debug($"Package {result.Message} uploaded.");

            return result;
        } // UploadPackage()

        /// <summary>
        /// Uploads the package from URL.
        /// </summary>
        /// <param name="folderId">The folder identifier.</param>
        /// <param name="details">The details.</param>
        /// <param name="groupName">The group name to chose while uploading the package.</param>
        /// <param name="description">The description.</param>
        /// <param name="accessLevel">The access level.</param>
        /// <param name="ignoreScm">if set to <c>true</c> ignore SCM files.</param>
        /// <returns>
        /// An <see cref="Result" /> object.
        /// </returns>
        /// <remarks>
        /// The message property of the result contains the upload id
        /// which is needed for further operations.
        /// </remarks>
        public Result UploadPackageFromUrl(
            int folderId,
            UrlUpload details,
            string groupName = "",
            string description = "",
            string accessLevel = "public",
            bool ignoreScm = true)
        {
            Log.Debug($"Uploading package {details.Name} from URL {details.Url} to folder {folderId}...");

            var request = new RestRequest(this.Url + "/uploads", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("folderId", folderId.ToString());
            request.AddHeader("groupName", groupName);
            request.AddHeader("uploadDescription", description);
            request.AddHeader("public", accessLevel);
            request.AddHeader("ignoreScm", ignoreScm.ToString());
            request.AddHeader("uploadType", "url");

            request.JsonSerializer = new JsonSerializer();
            var json = JsonConvert.SerializeObject(details);
            request.AddJsonBody(json);

            var resultRaw = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            Log.Debug($"Package {result.Message} uploaded.");

            return result;
        } // UploadPackageFromUrl()

        /// <summary>
        /// Uploads the package from a version control system.
        /// </summary>
        /// <param name="folderId">The folder identifier.</param>
        /// <param name="details">The details.</param>
        /// <param name="groupName">The group name to chose while uploading the package.</param>
        /// <param name="description">The description.</param>
        /// <param name="accessLevel">The access level.</param>
        /// <param name="ignoreScm">if set to <c>true</c> ignore SCM files.</param>
        /// <returns>
        /// An <see cref="Result" /> object.
        /// </returns>
        /// <remarks>
        /// The message property of the result contains the upload id
        /// which is needed for further operations.
        /// </remarks>
        public Result UploadPackageFromVcs(
            int folderId,
            VcsUpload details,
            string groupName = "",
            string description = "",
            string accessLevel = "public",
            bool ignoreScm = true)
        {
            Log.Debug($"Uploading package {details.VcsName} from {details.VcsUrl} to folder {folderId}...");
            var request = new RestRequest(this.Url + "/uploads", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("folderId", folderId.ToString());
            request.AddHeader("groupName", groupName);
            request.AddHeader("uploadDescription", description);
            request.AddHeader("public", accessLevel);
            request.AddHeader("ignoreScm", ignoreScm.ToString());
            request.AddHeader("uploadType", "vcs");

            request.JsonSerializer = new JsonSerializer();
            var json = JsonConvert.SerializeObject(details);
            request.AddJsonBody(json);

            var resultRaw = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            Log.Debug($"Package {result.Message} uploaded.");

            return result;
        } // UploadPackageFromVcs()

        /// <summary>
        /// Uploads the package from another FOSSology server.
        /// </summary>
        /// <param name="folderId">The folder identifier.</param>
        /// <param name="details">The details.</param>
        /// <param name="groupName">The group name to chose while uploading the package.</param>
        /// <param name="description">The description.</param>
        /// <param name="accessLevel">The access level.</param>
        /// <param name="ignoreScm">if set to <c>true</c> ignore SCM files.</param>
        /// <returns>
        /// An <see cref="Result" /> object.
        /// </returns>
        /// <remarks>
        /// The message property of the result contains the upload id
        /// which is needed for further operations.
        /// </remarks>
        public Result UploadPackageFromServer(
            int folderId,
            ServerUpload details,
            string groupName = "",
            string description = "",
            string accessLevel = "public",
            bool ignoreScm = true)
        {
            Log.Debug($"Uploading package {details.Name} from server {details.Path} to folder {folderId}...");
            var request = new RestRequest(this.Url + "/uploads", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("folderId", folderId.ToString());
            request.AddHeader("groupName", groupName);
            request.AddHeader("uploadDescription", description);
            request.AddHeader("public", accessLevel);
            request.AddHeader("ignoreScm", ignoreScm.ToString());
            request.AddHeader("uploadType", "server");

            request.JsonSerializer = new JsonSerializer();
            var json = JsonConvert.SerializeObject(details);
            request.AddJsonBody(json);

            var resultRaw = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            Log.Debug($"Package {result.Message} uploaded.");

            return result;
        } // UploadPackageFromServer()

        /// <summary>
        /// Determines whether the upload with the given id has been successfully
        /// uploaded and unpacked. Only when unpacking has been done, we can access
        /// the upload and trigger jobs.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if the upload has been successfully unpacked;
        /// otherwise, <c>false</c>.</returns>
        public bool IsUploadUnpacked(int id)
        {
            Log.Debug($"Checking status for upload {id}...");

            var result = this.api.Get(this.Url + $"/jobs?upload={id}");
            var jobs = JsonConvert.DeserializeObject<List<Job>>(
                result.Content,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });

            return jobs != null && jobs.All(job => job.Status == "Completed");
        } // IsUploadUnpacked()

        /// <summary>
        /// Gets the upload with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="groupName">The group name to choose.</param>
        /// <returns>A <see cref="Upload"/> object.</returns>
        public Upload GetUpload(int id, string groupName = "")
        {
            Log.Debug($"Getting upload {id}...");

            var request = new RestRequest(this.Url + $"/uploads/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var upload = JsonConvert.DeserializeObject<Upload>(
                    response.Content,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    });

                return upload;
            } // if

            // this will be the case for StatusCode == ServiceUnavailable
            // In this case header["look-at"] contains something like /api/v1/jobs?upload=8
            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            var exception = new FossologyApiException(
                ErrorCode.RestApiError, (HttpStatusCode)result.Code, result.Message, null);

            throw exception;
        } // GetUpload()

        /// <summary>
        /// Gets the summary for the upload with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="groupName">The group name to choose.</param>
        /// <returns>A <see cref="UploadSummary"/> object.</returns>
        public UploadSummary GetUploadSummary(int id, string groupName = "")
        {
            Log.Debug($"Getting upload summary {id}...");

            var request = new RestRequest(this.Url + $"/uploads/{id}/summary", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            var summary = JsonConvert.DeserializeObject<UploadSummary>(
                response.Content,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });

            return summary;
        } // GetUploadSummary()

        /// <summary>
        /// Gets the upload with the specified id.
        /// </summary>
        /// <returns>A list of <see cref="Upload"/> objects.</returns>
        /// <param name="groupName">The group name to use while retrieving uploads.</param>
        public IReadOnlyList<Upload> GetUploadList(string groupName = "")
        {
            Log.Debug("Getting all uploads...");

            var request = new RestRequest(this.Url + $"/uploads", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);

            var list = JsonConvert.DeserializeObject<List<Upload>>(
                response.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    });
            return list;
        } // GetUploadList()

        /// <summary>
        /// Gets the summary for the upload with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agent">Agent name, one of (nomos, monk, ninka, ojo).</param>
        /// <param name="containers">if set to <c>true</c> show directories and containers.</param>
        /// <returns>A list of <see cref="UploadLicenses" /> objects.</returns>
        public List<UploadLicenses> GetUploadLicenses(int id, string agent, bool containers)
        {
            Log.Debug($"Getting upload licenses for upload {id} and agent {agent}...");

            var ctext = containers ? "true" : "false";
            var request = new RestRequest(this.Url + $"/uploads/{id}/licenses?agent={agent}&containers={ctext}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = this.api.Execute(request);
            var summary = JsonConvert.DeserializeObject<List<UploadLicenses>>(
                response.Content,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });

            return summary;
        } // GetUploadLicenses()

        /// <summary>
        /// Deletes the upload with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="groupName">The group name to chose while deleting the package.</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        public Result DeleteUpload(int id, string groupName = "")
        {
            Log.Debug($"Deleting upload {id}...");

            var request = new RestRequest(this.Url + $"/uploads/{id}", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            return result;
        } // DeleteUpload()
    } // FossologyClient
}
