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
    using System.Linq;
    using System.Net;
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading.Tasks;
    using Fossology.Rest.Dotnet.Model;

    using Newtonsoft.Json;

    using RestSharp;
    using RestSharp.Extensions;

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
        /// <param name="details">The upload and scan details.</param>
        /// <returns>
        /// An <see cref="Result" /> object.
        /// </returns>
        /// <remarks>
        /// The message property of the result contains the upload id
        /// which is needed for further operations.
        /// </remarks>
        public Result UploadPackage(
            string fileName,
            int folderId,
            string groupName = "",
            Action uploadFinished = null,
            Action<float> uploadProgress = null,
            string description = "",
            string accessLevel = "public",
            bool ignoreScm = true,
            bool applyGlobal = false,
            UploadInformationFile details = null)
        {
            Log.Debug($"Uploading package {fileName} to folder {folderId}...");

            if (!System.IO.File.Exists(fileName))
            {
                throw new FossologyApiException(ErrorCode.FileNotFound, fileName);
            } // if

            var request = new RestRequest(this.Url + "/uploads", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("folderId", folderId.ToString());
            request.AddHeader("groupName", groupName);
            request.AddHeader("uploadDescription", description);
            request.AddHeader("public", accessLevel);
            request.AddHeader("ignoreScm", ignoreScm.ToString());
            request.AddHeader("uploadType", "file");
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("applyGlobal", applyGlobal.ToString());

            if (details != null)
            {
                var json = JsonConvert.SerializeObject(
                details,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
                request.AddJsonBody(json);
                request.AddHeader("Content-Type", "application/json");
            } // if

            var options = new FileParameterOptions();
            var fp = FileParameter.Create(
                "fileInput",
                () => new FileStreamWithProgress(fileName, uploadFinished, uploadProgress),
                fileName,
                "application/octet-stream",
                options);
            request.AddFile(fp, uploadFinished, uploadProgress);
            request.AlwaysMultipartFormData = true;
            var resultRaw = this.api.Execute(request);
            if (resultRaw?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            if (result == null)
            {
                Log.Error("Got empty response!");
            }
            else
            {
                Log.Debug($"Package {result.Message} uploaded.");
            } // if

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
            UploadInformationUrl details,
            string groupName = "",
            string description = "",
            string accessLevel = "public",
            bool ignoreScm = true)
        {
            Log.Debug($"Uploading package {details.Location.Name} from URL {details.Location.Url} to folder {folderId}...");

            var request = new RestRequest(this.Url + "/uploads", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("folderId", folderId.ToString());
            request.AddHeader("groupName", groupName);
            request.AddHeader("uploadDescription", description);
            request.AddHeader("public", accessLevel);
            request.AddHeader("ignoreScm", ignoreScm.ToString());
            request.AddHeader("uploadType", "url");

            var json = JsonConvert.SerializeObject(
                details,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            request.AddJsonBody(json);
            request.AddHeader("Content-Type", "application/json");

            var resultRaw = this.api.Execute(request);
            if (resultRaw?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            if (result == null)
            {
                Log.Error("Got empty response!");
            }
            else
            {
                Log.Debug($"Package {result.Message} uploaded.");
            } // if

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
            UploadInformationVcs details,
            string groupName = "",
            string description = "",
            string accessLevel = "public",
            bool ignoreScm = true)
        {
            Log.Debug($"Uploading package {details.Location.VcsName} from {details.Location.VcsUrl} to folder {folderId}...");
            var request = new RestRequest(this.Url + "/uploads", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("folderId", folderId.ToString());
            request.AddHeader("groupName", groupName);
            request.AddHeader("uploadDescription", description);
            request.AddHeader("public", accessLevel);
            request.AddHeader("ignoreScm", ignoreScm.ToString());
            request.AddHeader("uploadType", "vcs");

            var json = JsonConvert.SerializeObject(
                details,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            request.AddJsonBody(json);
            request.AddHeader("Content-Type", "application/json");

            var resultRaw = this.api.Execute(request);
            if (resultRaw?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            if (result == null)
            {
                Log.Error("Got empty response!");
            }
            else
            {
                Log.Debug($"Package {result.Message} uploaded.");
            } // if

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
            var request = new RestRequest(this.Url + "/uploads", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("folderId", folderId.ToString());
            request.AddHeader("groupName", groupName);
            request.AddHeader("uploadDescription", description);
            request.AddHeader("public", accessLevel);
            request.AddHeader("ignoreScm", ignoreScm.ToString());
            request.AddHeader("uploadType", "server");

            var json = JsonConvert.SerializeObject(details);
            request.AddJsonBody(json);
            request.AddHeader("Content-Type", "application/json");

            var resultRaw = this.api.Execute(request);
            if (resultRaw?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            if (result == null)
            {
                Log.Error("Got empty response!");
            }
            else
            {
                Log.Debug($"Package {result.Message} uploaded.");
            } // if

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
            if (result?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

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

            var request = new RestRequest(this.Url + $"/uploads/{id}");
            request.RequestFormat = DataFormat.Json;
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            if (response?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

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
            if (result == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            }
            else
            {
                throw new FossologyApiException(
                    ErrorCode.RestApiError, (HttpStatusCode)result.Code, result.Message, null);
            } // if
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

            var request = new RestRequest(this.Url + $"/uploads/{id}/summary");
            request.RequestFormat = DataFormat.Json;
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            if (response?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

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

            var request = new RestRequest(this.Url + "/uploads");
            request.RequestFormat = DataFormat.Json;
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            if (response?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

            var list = JsonConvert.DeserializeObject<List<Upload>>(
                response.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    });
            return list;
        } // GetUploadList()

        /// <summary>
        /// Gets the licenses for an upload.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agent">Agent name, one of (nomos, monk, ninka, ojo, scancode).</param>
        /// <param name="containers">if set to <c>true</c> show directories and containers.</param>
        /// <returns>A list of <see cref="UploadLicenses" /> objects.</returns>
        public List<UploadLicenses> GetUploadLicenses(int id, string agent, bool containers)
        {
            Log.Debug($"Getting upload licenses for upload {id} and agent {agent}...");

            var ctext = containers ? "true" : "false";
            var request = new RestRequest(this.Url + $"/uploads/{id}/licenses?agent={agent}&containers={ctext}");
            request.RequestFormat = DataFormat.Json;
            var response = this.api.Execute(request);
            if (response?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

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

            var request = new RestRequest(this.Url + $"/uploads/{id}", Method.Delete);
            request.RequestFormat = DataFormat.Json;
            if (!string.IsNullOrEmpty(groupName))
            {
                request.AddHeader("groupName", groupName);
            } // if

            var response = this.api.Execute(request);
            if (response?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            return result;
        } // DeleteUpload()

        /// <summary>
        /// Gets the upload file by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="filename">The filename.</param>
        /// <returns>
        /// An <see cref="Result" /> object.
        /// </returns>
        public Result GetUploadFileById(int id, string filename)
        {
            Log.Debug($"Downloading upload {id}...");

            var request = new RestRequest(this.Url + $"/uploads/{id}/download", Method.Get);
            var response = this.api.Execute(request);
            if (response?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

            if (response.StatusCode == HttpStatusCode.OK)
            {
                System.IO.File.WriteAllBytes(filename, response.RawBytes);
                var res = new Result();
                res.Code = (int)HttpStatusCode.OK;
                res.Message = string.Empty;
                return res;
            } // if

            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            if (result == null)
            {
                Log.Error("Got empty response!");
            }
            else
            {
                if (result.Code != (int)HttpStatusCode.OK)
                {
                    Log.Error($"Error downloading upload: {result.Message}");
                } // if
            } // if

            return result;
        } // GetUploadFileById()

        /// <summary>
        /// Gets the coüpyright for an upload.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A list of <see cref="CopyrightEntry" /> objects.</returns>
        public List<CopyrightEntry> GetUploadCopyrights(int id)
        {
            Log.Debug($"Getting upload copyrights for upload {id}...");

            var request = new RestRequest(this.Url + $"/uploads/{id}/copyrights");
            request.RequestFormat = DataFormat.Json;
            var response = this.api.Execute(request);
            if (response?.Content == null)
            {
                throw new FossologyApiException(ErrorCode.NoValidAnswer);
            } // if

            var summary = JsonConvert.DeserializeObject<List<CopyrightEntry>>(
                response.Content,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });

            return summary;
        } // GetUploadCopyrights()
    } // FossologyClient
}
