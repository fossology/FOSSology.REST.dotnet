#region Header
// ---------------------------------------------------------------------------
// <copyright file="FossologyClient.cs" company="Tethys">
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

namespace Fossology.Rest.Dotnet
{
    using System;
    using System.Collections.Generic;
    using System.IO;

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
    public class FossologyClient
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

        #region FOLDER SUPPORT
        /// <summary>
        /// Gets the folder with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Folder"/> object.</returns>
        public Folder GetFolder(int id)
        {
            Log.Debug($"Getting folder {id}...");

            var result = this.api.Get(this.Url + $"/folders/{id}");
            var folder = JsonConvert.DeserializeObject<Folder>(result.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
            return folder;
        } // GetFolder()

        /// <summary>
        /// Gets the list of all folders.
        /// </summary>
        /// <returns>A list of <see cref="Folder"/> objects.</returns>
        public IReadOnlyList<Folder> GetFolderList()
        {
            Log.Debug("Getting list of folder...");

            var result = this.api.Get(this.Url + "/folders");
            var list = JsonConvert.DeserializeObject<List<Folder>>(result.Content,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            return list;
        } // GetFolderList()

        /// <summary>
        /// Creates a new folder.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="parentFolder">The parent folder.</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        /// <remarks>
        /// The message property of the result contains the folder id
        /// which is needed for further operations.
        /// </remarks>>
        public Result CreateFolder(string folderName, int parentFolder)
        {
            var request = new RestRequest(this.Url + "/folders", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("parentFolder", parentFolder.ToString());
            request.AddHeader("folderName", folderName);

            var resultRaw = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            Log.Debug($"Folder {result.Message} created.");

            return result;
        } // CreateFolder()

        /// <summary>
        /// Deletes the folder with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        public Result DeleteFolder(int id)
        {
            Log.Debug($"Deleting folder {id}...");

            var response = this.api.Delete(this.Url + $"/folders/{id}");
            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            return result;
        } // DeleteFolder()
        #endregion // FOLDER SUPPORT

        #region UPLOAD SUPPORT
        /// <summary>
        /// Uploads the package.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="folderId">The folder identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="uploadFinished">The optional upload finished callback.</param>
        /// <param name="uploadProgress">The optional upload progress callback.</param>
        /// <param name="description">The description.</param>
        /// <param name="accessLevel">The access level.</param>
        /// <param name="ignoreScm">if set to <c>true</c> [ignore SCM].</param>
        /// <returns>
        /// An <see cref="Result" /> object.
        /// </returns>
        /// <remarks>
        /// The message property of the result contains the upload id
        /// which is needed for further operations.
        /// </remarks>
        /// &gt;
        public Result UploadPackage(string fileName, int folderId, int groupId,
                                     Action uploadFinished = null, Action<float> uploadProgress = null,
                                    string description = "", string accessLevel = "public",
                                    bool ignoreScm = true)
        {
            Log.Debug($"Uploading package {fileName} to folder {folderId}...");

            var request = new RestRequest(this.Url + "/uploads", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("folderId", folderId.ToString());
            request.AddHeader("groupId", groupId.ToString());
            request.AddHeader("uploadDescription", description);
            request.AddHeader("public", accessLevel);
            request.AddHeader("ignoreScm", ignoreScm.ToString());
            request.AddHeader("Content-Type", "multipart/form-data");

            var fi = new FileInfo(fileName);
            request.Files.Add(new FileParameter
                                  {
                                      Name = "fileInput",
                                      ContentLength = fi.Length,
                                      FileName = fileName,
                                      ContentType = "application/octet-stream",
                                      Writer =
                                          delegate(Stream stream)
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
                                              }
                                  });
            
            request.AlwaysMultipartFormData = true;
            var resultRaw = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<Result>(resultRaw.Content);
            Log.Debug($"Package {result.Message} uploaded.");

            return result;
        } // UploadPackage()

        /// <summary>
        /// Gets the upload with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Upload"/> object.</returns>
        public Upload GetUpload(int id)
        {
            Log.Debug($"Getting upload {id}...");

            var result = this.api.Get(this.Url + $"/uploads/{id}");
            var upload = JsonConvert.DeserializeObject<Upload>(result.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
            return upload;
        } // GetUpload()

        /// <summary>
        /// Gets the upload with the specified id.
        /// </summary>
        /// <returns>A list of <see cref="Upload"/> objects.</returns>
        public IReadOnlyList<Upload> GetUploadList()
        {
            Log.Debug("Getting all uploads...");

            var result = this.api.Get(this.Url + "/uploads");
            var list = JsonConvert.DeserializeObject<List<Upload>>(result.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
            return list;
        } // GetUploadList()

        /// <summary>
        /// Deletes the upload with the specified id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        public Result DeleteUpload(int id)
        {
            Log.Debug($"Deleting upload {id}...");

            var response = this.api.Delete(this.Url + $"/uploads/{id}");
            var result = JsonConvert.DeserializeObject<Result>(response.Content);
            return result;
        } // DeleteUpload()
        #endregion // UPLOAD SUPPORT

        #region JOB SUPPORT
        /// <summary>Triggers a job.</summary>
        /// <param name="folderId">The folder identifier.</param>
        /// <param name="uploadId">The upload identifier.</param>
        /// <param name="details">The details.</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        /// <remarks>
        /// The message property of the result contains the job id
        /// which is needed for further operations.
        /// </remarks>>
        public Result TriggerJob(int folderId, int uploadId, TriggerInfo details)
        {
            Log.Debug($"Triggering job for upload {uploadId}, folder={folderId}...");

            var json = JsonConvert.SerializeObject(details);
            var request = new RestRequest(this.Url + "/jobs", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            request.AddHeader("uploadId", uploadId.ToString());
            request.AddHeader("folderId", folderId.ToString());
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
            var job = JsonConvert.DeserializeObject<Job>(result.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
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
            var list = JsonConvert.DeserializeObject<List<Job>>(result.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
            return list;
        } // GetJobList()
        #endregion // JOB SUPPORT

        #region USER SUPPORT
        /// <summary>
        /// Gets a list of all users.
        /// </summary>
        /// <returns>A <see cref="User"/> object.</returns>
        public IReadOnlyList<User> GetUserList()
        {
            Log.Debug("Getting users...");

            var result = this.api.Get(this.Url + "/users");
            var list = JsonConvert.DeserializeObject<List<User>>(result.Content,
                new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
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
        #endregion // USER SUPPORT

        #region REPORT SUPPORT
        /// <summary>
        /// Triggers the report generation.
        /// </summary>
        /// <param name="uploadId">The upload identifier.</param>
        /// <param name="reportFormat">The report format.</param>
        /// <returns>An <see cref="Result"/> object.</returns>
        /// <remarks>
        /// The message property of the result contains the report id
        /// which is needed for further operations.
        /// </remarks>>
        public Result TriggerReportGeneration(int uploadId, string reportFormat)
        {
            Log.Debug($"Triggering report generation for upload {uploadId}, format={reportFormat}...");

            var request = new RestRequest(this.Url + "/report", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.AddHeader("uploadId", uploadId.ToString());
            request.AddHeader("reportFormat", reportFormat);

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
        /// <returns><c>true</c> if the file has been successfully downloaded; otherwise <c>false</c>.</returns>
        public bool DownloadReport(int reportId, string fileName)
        {
            Log.Debug($"Getting report {reportId}...");

            this.api.Get(this.Url + $"/report/{reportId}");

            // this could be an error response, in this case an exception would get thrown
            // If there is no exception, then we can normally download the file.
            this.api.DownloadFile(this.Url + $"/report/{reportId}", fileName);
            return true;
        } // DownloadReport()
        #endregion // REPORT SUPPORT

        #region SEARCH SUPPORT
        /// <summary>Search for a specific file.</summary>
        /// <param name="fileName">Filename to find, can contain % as wild-card</param>
        /// <param name="tag">Tag to find</param>
        /// <param name="searchType">Limit search, can be directory, containers, <c>allfiles</c></param>
        /// <param name="fileSizeMin">The file size minimum.</param>
        /// <param name="fileSizeMax">The file size maximum.</param>
        /// <param name="license">The license.</param>
        /// <param name="copyright">The copyright.</param>
        /// <returns>A list of <see cref="SearchResult"/> objects.</returns>
        public IReadOnlyList<SearchResult> Search(string fileName, string tag = null, 
                                                  string searchType = "allfiles",
                                                  int fileSizeMin = -1, int fileSizeMax = -1,
                                                  string license = null, string copyright = null)
        {
            Log.Debug($"Searching for file {fileName}...");

            var request = new RestRequest(this.Url + "/search", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonSerializer();
            request.Parameters.Clear();
            request.AddHeader("searchType", searchType);
            request.AddHeader("fileName", fileName);

            if (!string.IsNullOrEmpty(tag))
            {
                request.AddHeader("tag", tag);
            } // if

            if (fileSizeMin > -1)
            {
                request.AddHeader("filesizemin", fileSizeMin.ToString());
            } // if

            if (fileSizeMax > -1)
            {
                request.AddHeader("filesizemax", fileSizeMax.ToString());
            } // if

            if (!string.IsNullOrEmpty(license))
            {
                request.AddHeader("license", license);
            } // if

            if (!string.IsNullOrEmpty(license))
            {
                request.AddHeader("copyright", copyright);
            } // if
            
            var response = this.api.Execute(request);
            var result = JsonConvert.DeserializeObject<IReadOnlyList<SearchResult>>(response.Content);

            return result;
        } // Search()
        #endregion // SEARCH SUPPORT
        #endregion // PUBLIC METHODS
    } // FossologyClient
}
