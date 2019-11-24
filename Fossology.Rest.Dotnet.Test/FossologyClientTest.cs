#region Header
// ---------------------------------------------------------------------------
// <copyright file="FossologyClientTest.cs" company="Tethys">
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

namespace Fossology.Rest.Dotnet.Test
{
    using System.Diagnostics;
    using System.IO;
    using System.Net;

    using Fossology.Rest.Dotnet;
    using Fossology.Rest.Dotnet.Model;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Tethys.Logging;
    using Tethys.Logging.Console;

    /// <summary>
    /// Unit test class.
    /// </summary>
    [TestClass]
    public class FossologyClientTest
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The Fossology url.
        /// Take care: the token depends also on the url!
        /// </summary>
        private const string LocalUrl = "http://localhost:8081/repo/api/v1";

        /// <summary>
        /// The access token.
        /// </summary>
        private const string Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJsb2NhbGhvc3QiLCJhdWQiOiJsb2NhbGhvc3QiLCJleHAiOjE1NzUxNTgzOTksIm5iZiI6MTU3NDU1MzYwMCwianRpIjoiTWk0eiIsInNjb3BlIjoid3JpdGUifQ.vubUlZv2u_9naEU1VAkAPWhS0Ccn8tVnbNNURlQyDko";

        /// <summary>
        /// The filename of a test package.
        /// </summary>
        private const string Filename = @"..\..\..\TestData\fetch-retry-master.zip";
        #endregion // PRIVATE PROPERTIES

        /// <summary>Test class initialization.</summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", 
            "IDE0060:Remove unused parameter", Justification = "Parameter is required")]
        public static void Initialize(TestContext context)
        {
            LogManager.Adapter = new ColoredConsoleLoggerFactoryAdapter(LogLevel.Debug);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetVersion()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetVersion();
            Assert.IsNotNull(result);
            Debug.WriteLine($"Version = {result}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestErrorInvalidToken()
        {
            var client = new FossologyClient(LocalUrl,
                "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiIxOTIuMTY4LjAuMTc4IiwiYXVkIjoiMTkyLjE2OC4wLjE3OCIsImV4cCI6MTU3NTkzNTk5OSwibmJmIjoxNTczMzQ0MDAwLCJqdGkiOiJOUzR6Iiwic2NvcGUiOiJ3cml0ZSJ9.F9FsMOdvcBKnVoUX87EYTmzcVI5dtutJN-cnPgIk0VE");
            try
            {
                client.GetFolderList();
            }
            catch (FossologyApiException fex)
            {
                Assert.AreEqual(ErrorCode.RestApiError, fex.ErrorCode);
                Assert.AreEqual(HttpStatusCode.Forbidden, fex.HttpStatusCode);
            }
            
            Assert.IsFalse(false, "No valid error response");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetFolder()
        {
            const int Id = 1;

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetFolder(Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(Id, result.Id);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetFolderList()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetFolderList();
            Assert.IsNotNull(result);
            foreach (var folder in result)
            {
                Debug.WriteLine($"  Folder = {folder}");
            }
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestCreateFolder()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var listPrev = client.GetFolderList();
            var result = client.CreateFolder("TestFolder", 1);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            if (result.Code == 200)
            {
                // folder already exists
                return;
            }

            Assert.AreEqual(201, result.Code);
            var listCurr = client.GetFolderList();
            Assert.AreEqual(listPrev.Count + 1, listCurr.Count);
            Debug.WriteLine($"Created folder id = {result.Message}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeleteFolder()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.DeleteFolder(3);
            Assert.IsNotNull(result);
            Assert.AreEqual(202, result.Code);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestUploadPackage()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.UploadPackage(Filename, 3, 0);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            Debug.WriteLine($"Upload id = {result.Message}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetUpload()
        {
            const int Id = 2;

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetUpload(Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(Id, result.Id);
            Debug.WriteLine($"Upload = {result}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetUploadList()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetUploadList();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            foreach (var upload in result)
            {
                Debug.WriteLine($"  Upload = {upload}");
            }
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeleteUpload()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.DeleteUpload(3);
            Assert.IsNotNull(result);
            Assert.AreEqual(202, result.Code);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestTriggerJob()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var job = new TriggerInfo();
            job.Analysis.Bucket = true;
            job.Analysis.CopyrightEmailAuthor = true;
            job.Analysis.Ecc = true;
            job.Analysis.Keyword = true;
            job.Analysis.Mime = true;
            job.Analysis.Monk = true;
            job.Analysis.Nomos = true;
            job.Analysis.Ojo = true;
            job.Analysis.Package = true;
            job.Decider.NomosMonk = true;
            job.Decider.BulkReused = true;
            job.Decider.NewScanner = false;
            job.Decider.OjoDecider = false;
            job.Reuse.ReuseUploadId = 0;
            job.Reuse.ReuseGroup = 0;
            job.Reuse.ReuseMain = false;
            job.Reuse.ReuseEnhanced = false;
            var result = client.TriggerJob(4, 3, job);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            Debug.WriteLine($"TriggerInfo job id = {result.Message}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetJob()
        {
            const int Id = 4;

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetJob(Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(Id, result.Id);
            Debug.WriteLine($"Job = {result}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetJobList()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetJobList();
            Assert.IsNotNull(result);
            foreach (var job in result)
            {
                Debug.WriteLine($"  Job = {job}");
            }
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestTriggerReport()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.TriggerReportGeneration(2, "spdx2");
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            Debug.WriteLine($"Created report id = {result.Message}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDownloadReport()
        {
            const string ReportName = "Report.spdx2.rdf";

            if (File.Exists(ReportName))
            {
                File.Delete(ReportName);
            } // if

            var client = new FossologyClient(LocalUrl, Token);
            client.DownloadReport(5, ReportName);
            Assert.IsTrue(File.Exists(ReportName));
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetUserList()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetUserList();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            foreach (var user in result)
            {
                Debug.WriteLine($"  User = {user}");
            }
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetUser()
        {
            const int Id = 3;

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetUser(Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(Id, result.Id);
            Debug.WriteLine($"User = {result}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeleteUserSuccess()
        {
            const int Id = 4;

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.DeleteUser(Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(202, result.Code);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeleteUserFailure()
        {
            const int Id = 44;

            var client = new FossologyClient(LocalUrl, Token);

            try
            {
                client.DeleteUser(Id);
            }
            catch (FossologyApiException fex)
            {
                Assert.AreEqual(ErrorCode.RestApiError, fex.ErrorCode);
                Assert.AreEqual(HttpStatusCode.NotFound, fex.HttpStatusCode);
            }

            Assert.IsFalse(false, "No valid error response");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestSearch()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.Search("%");
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Integration test, runs unit tests in defined order on a plain
        /// Fossology instance.
        /// </summary>
        [TestMethod]
        public void MyIntegrationTestLikeUnitTest()
        {
            const string ReportFilename = "Report.spdx2.rdf";

            var client = new FossologyClient(LocalUrl, Token);

            var version = client.GetVersion();
            Assert.IsNotNull(version);
            Debug.WriteLine($"Version = {version}");

            var folderlist = client.GetFolderList();
            Assert.IsNotNull(folderlist);
            Assert.AreEqual(1, folderlist.Count);

            var folder = client.GetFolder(1);
            Assert.IsNotNull(folder);
            Assert.AreEqual(1, folder.Id);

            var result = client.CreateFolder("TestFolder", 1);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            Assert.AreEqual("3", result.Message);

            result = client.UploadPackage(Filename, 3, 0);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            Assert.AreEqual("2", result.Message);

            var uploadlist = client.GetUploadList();
            Assert.IsNotNull(uploadlist);
            Assert.AreEqual(1, uploadlist.Count);

            var upload = client.GetUpload(2);
            Assert.IsNotNull(upload);
            Assert.AreEqual(2, upload.Id);

            var userlist = client.GetUserList();
            Assert.IsNotNull(userlist);
            Assert.AreEqual(2, userlist.Count);

            var user = client.GetUser(2);
            Assert.IsNotNull(user);
            Assert.AreEqual(2, user.Id);

            var searchresult = client.Search("%");
            Assert.IsNotNull(searchresult);
            Assert.IsTrue(searchresult.Count > 0);
            
            var jobTrigger = new TriggerInfo();
            jobTrigger.Analysis.Bucket = true;
            jobTrigger.Analysis.CopyrightEmailAuthor = true;
            jobTrigger.Analysis.Ecc = true;
            jobTrigger.Analysis.Keyword = true;
            jobTrigger.Analysis.Mime = true;
            jobTrigger.Analysis.Monk = true;
            jobTrigger.Analysis.Nomos = true;
            jobTrigger.Analysis.Ojo = true;
            jobTrigger.Analysis.Package = true;
            jobTrigger.Decider.NomosMonk = true;
            jobTrigger.Decider.BulkReused = true;
            jobTrigger.Decider.NewScanner = false;
            jobTrigger.Decider.OjoDecider = false;
            jobTrigger.Reuse.ReuseUploadId = 0;
            jobTrigger.Reuse.ReuseGroup = 0;
            jobTrigger.Reuse.ReuseMain = false;
            jobTrigger.Reuse.ReuseEnhanced = false;
            result = client.TriggerJob(3, 2, jobTrigger);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            var jobId = int.Parse(result.Message);
            
            var joblist = client.GetJobList();
            Assert.IsNotNull(joblist);
            Assert.IsTrue(joblist.Count > 0);

            var job = client.GetJob(jobId);
            Assert.IsNotNull(job);
            Assert.AreEqual(jobId, job.Id);

            result = client.TriggerReportGeneration(2, "spdx2");
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);

            if (File.Exists(ReportFilename))
            {
                File.Delete(ReportFilename);
            } // if

            client.DownloadReport(5, ReportFilename);
            Assert.IsTrue(File.Exists(ReportFilename));

            result = client.DeleteUpload(2);
            Assert.IsNotNull(result);
            Assert.AreEqual(202, result.Code);

            result = client.DeleteFolder(3);
            Assert.IsNotNull(result);
            Assert.AreEqual(202, result.Code);
        }
    }
}
