// ---------------------------------------------------------------------------
// <copyright file="FossologyClientTest.cs" company="Tethys">
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

namespace Fossology.Rest.Dotnet.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Threading;
    using Fossology.Rest.Dotnet;
    using Fossology.Rest.Dotnet.Model;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tethys.Logging;
    using Tethys.Logging.Console;

    // NOTE:
    // 1. Prior to running test, valid token information needs to be provided.

    /// <summary>
    /// Unit test class.
    /// </summary>
    [TestClass]
    public class FossologyClientTest
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The Fossology url.
        /// Take care: the token depends also on the url.
        /// </summary>
        private const string LocalUrl = "http://localhost:8081/repo/api/v1";

        /// <summary>
        /// The access token.
        /// </summary>
        private const string Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2NzI2MTc1OTksIm5iZiI6MTY3MjM1ODQwMCwianRpIjoiTWk0eiIsInNjb3BlIjoid3JpdGUifQ.zivhU2CiTDI2_PqWvPejifhs6d6HohVOW6w1XG3GUSQ";

        /// <summary>
        /// The filename of a test package.
        /// </summary>
        private const string Filename = @"..\..\..\..\TestData\fetch-retry-master.zip";

        /// <summary>
        /// The name of a test package.
        /// </summary>
        private const string PackageName = "fetch-retry-master.zip";

        /// <summary>
        /// The name of a test package.
        /// </summary>
        private const string PackageName2 = "Tethys.xml_v1.0.0.zip";

        /// <summary>
        /// The test folder name.
        /// </summary>
        private const string TestFolderName = "TestFolder";
        #endregion // PRIVATE PROPERTIES

        /// <summary>Test class initialization.</summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Style", "IDE0060:Remove unused parameter", Justification = "Parameter is required")]
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
        public void TestGetToken()
        {
            var client = new FossologyClient(LocalUrl, string.Empty);
            var request = new TokenRequest();
            request.Username = "fossy";
            request.Password = "fossy";
            var uuid = Guid.NewGuid().ToString();
            request.TokenName = uuid;
            request.TokenScope = "write";
            request.TokenExpire = DateTime.Today.AddDays(3).ToString("yyyy-MM-dd");
            var result = client.GetToken(request);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 20);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestErrorInvalidToken()
        {
            var client = new FossologyClient(
                LocalUrl,
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
            var uuid = Guid.NewGuid().ToString();
            var result = client.CreateFolder("TestFolder-" + uuid, 1);
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
        public void TestCreateFolderForGroup()
        {
            var groupName = "TestFolderGroup-" + Guid.NewGuid();

            var client = new FossologyClient(LocalUrl, Token);
            var actual = client.CreateGroup(groupName);
            Assert.IsNotNull(actual);
            Assert.AreEqual(200, actual.Code);

            var uuid = Guid.NewGuid().ToString();
            var result = client.CreateFolder("TestFolder-" + uuid, 1, groupName);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeleteFolder()
        {
            var client = new FossologyClient(LocalUrl, Token);

            var folderName = "TestFolder-" + Guid.NewGuid();
            Assert.IsFalse(FossologyFolderExists(client, folderName));

            var result = client.CreateFolder(folderName, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.Code);
            var folderId = int.Parse(result.Message);
            Assert.IsTrue(FossologyFolderExists(client, folderName));

            result = client.DeleteFolder(folderId);
            Assert.IsNotNull(result);
            Assert.AreEqual(202, result.Code);
            Assert.IsFalse(FossologyFolderExists(client, folderName));
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestUploadPackage()
        {
            var folderId = EnsureTestFolderExists();

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.UploadPackage(Filename, folderId);
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
            var id = FindUpload(PackageName2);
            if (id < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);
                id = FindUpload(PackageName2);
            } // if

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetUpload(id);
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Debug.WriteLine($"Upload = {result}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestUploadPackageAndCheckLookAt()
        {
            var id = FindUpload(PackageName2);
            if (id < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);
                id = FindUpload(PackageName2);
            } // if

            var client = new FossologyClient(LocalUrl, Token);

            try
            {
                var result2 = client.GetUpload(id);
                Assert.IsNotNull(result2);
                Assert.AreEqual(id, result2.Id);
            }
            catch (FossologyApiException ex)
            {
                Debug.WriteLine("The ununpack agent has not started yet.");
                Debug.WriteLine($"{ex.Message}");
            } // catch
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestUploadPackageCheckUnpackStatus()
        {
            var id = FindUpload(PackageName2);
            if (id < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);
                id = FindUpload(PackageName2);
            } // if

            var client = new FossologyClient(LocalUrl, Token);
            Debug.WriteLine($"Upload id = {id}");

            WaitUntilUploadIsDone(client, id);
            Debug.WriteLine($"Upload id = {id} is now available");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestUploadPackageFromUrl()
        {
            var folderId = EnsureTestFolderExists();

            var client = new FossologyClient(LocalUrl, Token);
            var details = new UrlUpload();
            details.Name = "Tethys.xml_v1.0.0.zip";
            details.Url = "https://github.com/tngraf/Tethys.Xml/archive/v1.0.0.zip";
            details.MaxRecursionDepth = 0;

            var result = client.UploadPackageFromUrl(folderId, details);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            Debug.WriteLine($"Upload id = {result.Message}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestUploadPackageFromVcs()
        {
            var folderId = EnsureTestFolderExists();

            var client = new FossologyClient(LocalUrl, Token);
            var details = new VcsUpload();
            details.VcsName = "Tethys.Logging";
            details.VcsUrl = "https://github.com/tngraf/Tethys.Logging.git";
            details.VcsBranch = "master";
            details.VcsType = "git";
            details.VcsUsername = "xxx";
            details.VcsPassword = "xxx";

            var result = client.UploadPackageFromVcs(folderId, details);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            Debug.WriteLine($"Upload id = {result.Message}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetUploadSummary()
        {
            var id = FindUpload(PackageName2);
            if (id < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);
                id = FindUpload(PackageName2);
            } // if

            var client = new FossologyClient(LocalUrl, Token);
            var summary = client.GetUploadSummary(id);
            Assert.IsNotNull(summary);
            Assert.AreEqual(id, summary.Id);
            Debug.WriteLine($"Upload = {summary}");
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
            foreach (var upload in result)
            {
                Debug.WriteLine($"  Upload summary = {upload}");
            }
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetUploadLicenses()
        {
#if true
            var uploadId = FindUpload(PackageName2);
            if (uploadId < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);
                uploadId = FindUpload(PackageName2);
            } // if

            UploadPackageAndRunJobs(uploadId);
#else
            // use a specific upload
            var uploadId = 111;
#endif

            var client = new FossologyClient(LocalUrl, Token);
            var licenses = client.GetUploadLicenses(uploadId, "nomos", true);
            Assert.IsNotNull(licenses);

            ////Assert.AreEqual(14, licenses.Count);
            ////Assert.AreEqual(1, licenses[2].Findings.Scanner.Count);
            ////Assert.AreEqual("Apache-2.0", licenses[2].Findings.Scanner[0]);
            ////Assert.AreEqual("Tethys.xml_v1.0.0.zip/Tethys.Xml-1.0.0/LICENSE", licenses[2].FilePath);

            licenses = client.GetUploadLicenses(uploadId, "monk", true);
            Assert.IsNotNull(licenses);
            foreach (var info in licenses)
            {
                Debug.WriteLine($"Upload licenses found by monk = {info}");
            } // foreach
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeleteUpload()
        {
            var id = FindUpload(PackageName2);
            if (id < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);
                id = FindUpload(PackageName2);
            } // if

            if (id < 0)
            {
                Assert.Inconclusive("Upload not ready...");
            } // if

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.DeleteUpload(id);
            Assert.IsNotNull(result);
            Assert.AreEqual(202, result.Code);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestTriggerJob()
        {
            var folderId = EnsureTestFolderExists();
            var id = FindUpload(PackageName2);
            if (id < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);
                id = FindUpload(PackageName2);
            } // if

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
            job.Decider.NewScanner = true;
            job.Decider.OjoDecider = true;
            job.Reuse.ReuseUploadId = 0;
            job.Reuse.ReuseGroup = 0;
            job.Reuse.ReuseMain = true;
            job.Reuse.ReuseEnhanced = true;
            var result = client.TriggerJob(folderId, id, job, "fossy");
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            Debug.WriteLine($"TriggerInfo job id = {result.Message}");
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
        public void TestGetJobListWithId()
        {
            var uploadId = FindUpload(PackageName2);
            if (uploadId < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);
                uploadId = FindUpload(PackageName2);
            } // if

            var jobId = UploadPackageAndRunJobs(uploadId);

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetJobList(uploadId, 1, 1000);
            var found = false;
            foreach (var job in result)
            {
                if (job.Id == jobId)
                {
                    found = true;
                    break;
                } // if
            } // foreach

            Assert.IsTrue(found, "Expected job not found!");

            result = client.GetJobList(-1, 1, 1000);
            Assert.IsNotNull(result);
            found = false;
            foreach (var job in result)
            {
                if (job.Id == jobId)
                {
                    found = true;
                    break;
                } // if
            } // foreach

            Assert.IsTrue(found, "Expected job not found!");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetJob()
        {
            var uploadId = FindUpload(PackageName2);
            if (uploadId < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);
                uploadId = FindUpload(PackageName2);
            } // if

            var jobId = UploadPackageAndRunJobs(uploadId);

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.GetJob(jobId);
            Assert.IsNotNull(result);
            Assert.AreEqual(jobId, result.Id);
            Debug.WriteLine($"Job = {result}");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestTriggerReport()
        {
            var uploadId = FindUpload(PackageName2);
            if (uploadId < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);
                uploadId = FindUpload(PackageName2);
            } // if

            UploadPackageAndRunJobs(uploadId);

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.TriggerReportGeneration(uploadId, "spdx2");
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

            if (System.IO.File.Exists(ReportName))
            {
                System.IO.File.Delete(ReportName);
            } // if

            var uploadId = FindUpload(PackageName2);
            if (uploadId < 0)
            {
                this.TestUploadPackageFromUrl();
                Thread.Sleep(3000);
                uploadId = FindUpload(PackageName2);
            } // if

            UploadPackageAndRunJobs(uploadId);

            var reportId = TriggerReportGeneration(uploadId);

            // ugly but required: wait some time until report is available
            Thread.Sleep(3000);

            var client = new FossologyClient(LocalUrl, Token);
            client.DownloadReport(reportId, ReportName);
            Assert.IsTrue(System.IO.File.Exists(ReportName));
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
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestSearch2()
        {
            var uploadId = FindUpload(PackageName2);
            if (uploadId < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);
                uploadId = FindUpload(PackageName2);
            } // if

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.Search("%", null, "allfiles", 10, 10000000);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);

            result = client.Search("%BuildPackages.ps1%", null, "allfiles", 10, 10000000, null, null);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.AreEqual("BuildPackages.ps1", result[0].Filename);
        }

        /// <summary>
        /// Integration test, runs unit tests in defined order on a plain
        /// Fossology instance.
        /// </summary>
        [TestMethod]
        public void MyIntegrationTestLikeUnitTest()
        {
            const string ReportFilename = "Report.spdx2.rdf";

            var client = new FossologyClient(LocalUrl, string.Empty);

            var version = client.GetVersion();
            Assert.IsNotNull(version);
            Debug.WriteLine($"Version = {version}");

            var request = new TokenRequest();
            var guid = Guid.NewGuid();
            request.Username = "fossy";
            request.Password = "fossy";
            request.TokenName = guid.ToString();
            request.TokenScope = "write";
            request.TokenExpire = DateTime.Today.AddDays(3).ToString("yyyy-MM-dd");
            var tokenResult = client.GetToken(request);
            Assert.IsNotNull(tokenResult);
            Assert.IsTrue(tokenResult.Length > 20);

            client = new FossologyClient(LocalUrl, tokenResult);
            var folderId = EnsureTestFolderExists();

#if false
            var result = client.UploadPackage(Filename, folderId);
#else
            var uploadId = FindUpload(PackageName2);
            if (uploadId < 0)
            {
                this.TestUploadPackageFromUrl();
                // ugly but required: wait some time until report is available
                Thread.Sleep(3000);

                uploadId = FindUpload(PackageName2);
            } // if
#endif
            Assert.IsTrue(uploadId > 0);

            WaitUntilUploadIsDone(client, uploadId);

            var uploadlist = client.GetUploadList();
            Assert.IsNotNull(uploadlist);
            Assert.IsTrue(uploadlist.Count > 0);

            var upload = client.GetUpload(uploadId);
            Assert.IsNotNull(upload);
            Assert.AreEqual(uploadId, upload.Id);

            var userlist = client.GetUserList();
            Assert.IsNotNull(userlist);
            Assert.AreEqual(2, userlist.Count);

            var user = client.GetUser(2);
            Assert.IsNotNull(user);
            Assert.AreEqual(2, user.Id);

#if false
            var searchresult = client.Search("%");
            Assert.IsNotNull(searchresult);
            Assert.IsTrue(searchresult.Count > 0);
#endif

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
            var result = client.TriggerJob(folderId, uploadId, jobTrigger);
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
            WaitUntilJobIsDone(client, jobId);

            // PHP Fatal error:  Uncaught Exception: cannot find uploadId = 14 in /usr/local/share/fossology/lib/php/Dao/UploadDao.php:201
            var summary = client.GetUploadSummary(uploadId);
            Assert.IsNotNull(summary);
            Assert.AreEqual(uploadId, summary.Id);

            var licensesFound = client.GetUploadLicenses(
                uploadId, "nomos", true);
            Assert.IsNotNull(licensesFound);
            Assert.IsTrue(licensesFound.Count > 2);
#if false
            if (licensesFound[2].AgentFindings.Count > 0)
            {
                Assert.AreEqual("Apache-2.0", licensesFound[2].AgentFindings[0]);
            } // if
#endif

            result = client.TriggerReportGeneration(uploadId, "spdx2");
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            // extract report id
            var text = result.Message.Substring(result.Message.LastIndexOf('/') + 1);
            var reportId = int.Parse(text);

            if (System.IO.File.Exists(ReportFilename))
            {
                System.IO.File.Delete(ReportFilename);
            } // if

            // ugly but required: wait some time until report is available
            Thread.Sleep(3000);

            client.DownloadReport(reportId, ReportFilename);
            Assert.IsTrue(System.IO.File.Exists(ReportFilename));

            result = client.DeleteUpload(uploadId);
            Assert.IsNotNull(result);
            Assert.AreEqual(202, result.Code);

            result = client.DeleteFolder(folderId);
            Assert.IsNotNull(result);
            Assert.AreEqual(202, result.Code);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetHealth()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var actual = client.GetHealth();
            Assert.IsNotNull(actual);
            Assert.AreEqual("OK", actual.Status);
            Assert.AreEqual("OK", actual.Scheduler.Status);
            Assert.AreEqual("OK", actual.Database.Status);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetInfo()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var actual = client.GetInfo();
            Assert.IsNotNull(actual);
            Assert.AreEqual("FOSSology API", actual.Name);
            Assert.AreEqual("fossology@fossology.org", actual.Contact);
            Assert.AreEqual("GPL-2.0-only", actual.FossologyLicense.Name);
            Assert.AreEqual("master", actual.FossologyInfo.BranchName);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetGroupList()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var actual = client.GetGroupList();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestCreateGroup()
        {
            var groupName = "TestGroup-" + Guid.NewGuid();

            var client = new FossologyClient(LocalUrl, Token);
            var actual = client.CreateGroup(groupName);
            Assert.IsNotNull(actual);
            Assert.AreEqual(200, actual.Code);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetLicenseList()
        {
            var client = new FossologyClient(LocalUrl, Token);

            // default
            var actual = client.GetLicenseList();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 100);

            actual = client.GetLicenseList(1, 1, "main", true);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 1);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetLicense()
        {
            var client = new FossologyClient(LocalUrl, Token);

            var actual = client.GetLicense("MIT");
            Assert.IsNotNull(actual);
            Assert.AreEqual("MIT", actual.ShortName);
            Assert.AreEqual("MIT License", actual.FullName);
            Assert.AreEqual(0, actual.Risk);
            Assert.IsFalse(actual.IsCandidate);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestGetLicenseWithObligations()
        {
            var client = new FossologyClient(LocalUrl, Token);

            var actual = client.GetLicense("Apache-2.0");
            Assert.IsNotNull(actual);
            Assert.AreEqual("Apache-2.0", actual.ShortName);
            Assert.AreEqual(0, actual.Risk);
            Assert.IsFalse(actual.IsCandidate);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestCreateLicense_Fail()
        {
            var client = new FossologyClient(LocalUrl, Token);

            var license = new License();
            license.ShortName = "MIT";
            license.FullName = "MIT License";

            try
            {
                client.CreateLicense(license);
                Assert.IsFalse(true, "This must not happen!");
            }
            catch (FossologyApiException fex)
            {
                Assert.IsNotNull(fex);
                Assert.AreEqual(HttpStatusCode.Conflict, fex.HttpStatusCode);
            }
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestCreateLicense()
        {
            var client = new FossologyClient(LocalUrl, Token);

            var license = new License();
            var uuid = Guid.NewGuid().ToString();
            license.ShortName = "TOM-MIT-" + uuid;
            license.FullName = "Tom's MIT License-" + uuid;
            license.IsCandidate = true;
            license.Risk = 0;
            license.LicenseText = "Some dummy license text " + uuid;

            var actual = client.CreateLicense(license);
            Assert.IsNotNull(actual);
            Assert.AreEqual(201, actual.Code);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestFileSearch()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var hash = new SearchHash();
            hash.Sha1 = null;
            hash.Md5 = "3F40923DFBB69F90727DFE7378B8E962";
            hash.Sha256 = null;
            var hashes = new List<SearchHash>();
            hashes.Add(hash);

            var actual = client.SearchForFile(hashes);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);

            // always returns
            // [
            //   {
            //     "hash" : {
            //        "sha1":null,
            //        "md5":null,
            //        "sha256":null,
            //        "size":null
            //      },
            //     "message": "Invalid keys"
            //   }
            // ]"
        }

        //// ---------------------------------------------------------------------

        #region TESTS THAT REQUIRE MANUAL PREPARATION
#if false
        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeleteUserSuccess()
        {
            // since there is no way to automatically add a user,
            // you always need to have a user ready to be deleted
            const int Id = 4;

            var client = new FossologyClient(LocalUrl, Token);
            var result = client.DeleteUser(Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(202, result.Code);
        }
#endif
        #endregion TESTS THAT REQUIRE MANUAL PREPARATION

        //// ---------------------------------------------------------------------

        #region SUPPORT METHODS
        /// <summary>
        /// Triggers the report generation.
        /// </summary>
        /// <param name="uploadId">The upload identifier.</param>
        /// <returns>The report id.</returns>
        private static int TriggerReportGeneration(int uploadId)
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.TriggerReportGeneration(uploadId, "spdx2");
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);

            var text = result.Message[(result.Message.LastIndexOf('/') + 1) ..];
            return int.Parse(text);
        }

        /// <summary>
        /// Uploads a package and run jobs.
        /// </summary>
        /// <param name="uploadId">The upload identifier.</param>
        /// <returns>The job id.</returns>
        private static int UploadPackageAndRunJobs(int uploadId)
        {
            var folderId = EnsureTestFolderExists();
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
            job.Decider.NewScanner = true;
            job.Decider.OjoDecider = true;
            job.Reuse.ReuseUploadId = 0;
            job.Reuse.ReuseGroup = 0;
            job.Reuse.ReuseMain = true;
            job.Reuse.ReuseEnhanced = true;
            var result = client.TriggerJob(folderId, uploadId, job, "fossy");
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            Debug.WriteLine($"TriggerInfo job id = {result.Message}");

            var jobId = int.Parse(result.Message);

            var joblist = client.GetJobList();
            Assert.IsNotNull(joblist);
            Assert.IsTrue(joblist.Count > 0);

            var jobInfo = client.GetJob(jobId);
            Assert.IsNotNull(jobInfo);
            Assert.AreEqual(jobId, jobInfo.Id);
            WaitUntilJobIsDone(client, jobId);

            return jobId;
        }

        /// <summary>
        /// Finds the id of the upload with the given name.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>System.Int32.</returns>
        private static int FindUpload(string filename)
        {
            var client = new FossologyClient(LocalUrl, Token);
            var uploadList = client.GetUploadList();
            foreach (var upload in uploadList)
            {
                if (upload.UploadName == filename)
                {
                    return upload.Id;
                } // if
            } // foreach

            return -1;
        }

        /// <summary>
        /// Ensures that the test folder exists.
        /// </summary>
        /// <returns>The test folder id.</returns>
        private static int EnsureTestFolderExists()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var folderlist = client.GetFolderList();
            Assert.IsNotNull(folderlist);
            if (folderlist.Count < 1)
            {
                Assert.Fail("Invalid number of folders");
            } // if

            var folder = client.GetFolder(1);
            Assert.IsNotNull(folder);
            Assert.AreEqual(1, folder.Id);

            var folderId = FindFolder(folderlist, TestFolderName);
            if (folderId < 0)
            {
                var result = client.CreateFolder(TestFolderName, 1);
                Assert.IsNotNull(result);
                Assert.AreEqual("INFO", result.Type);
                Assert.AreEqual(201, result.Code);
                folderId = int.Parse(result.Message);
                Assert.IsTrue(folderId > 0);
            } // if

            return folderId;
        }

        /// <summary>
        /// Tests whether the specified folder exists.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns><c>true</c> if the folder exists, <c>false</c> otherwise.</returns>
        private static bool FossologyFolderExists(FossologyClient client, string folderName)
        {
            var list = client.GetFolderList();
            foreach (var folder in list)
            {
                if (folder.Name == folderName)
                {
                    return true;
                } // if
            } // foreach

            return false;
        }

        /// <summary>
        /// Waits until the upload is done.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="id">The identifier.</param>
        private static void WaitUntilUploadIsDone(FossologyClient client, int id)
        {
            while (!client.IsUploadUnpacked(id))
            {
                Debug.WriteLine($"Waiting for upload {id} to get unpacked...");
                Thread.Sleep(500);
            } // while
        }

        /// <summary>
        /// Waits the until the given job is done.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="id">The job identifier.</param>
        private static void WaitUntilJobIsDone(FossologyClient client, int id)
        {
            while (true)
            {
                var job = client.GetJob(id);
                if (job.Status == "Completed")
                {
                    return;
                } // if

                Debug.WriteLine($"Waiting for job {id} to complete...");
                Thread.Sleep(500);
            } // while
        }

        /// <summary>
        /// Finds the folder with the given name.
        /// </summary>
        /// <param name="folderList">The folder list.</param>
        /// <param name="folderName">The folder name.</param>
        /// <returns>The id of the folder or -1.</returns>
        private static int FindFolder(IEnumerable<Folder> folderList, string folderName)
        {
            foreach (var folder in folderList)
            {
                if (folder.Name == folderName)
                {
                    return folder.Id;
                } // if
            } // foreach

            return -1;
        } // FindFolder()
        #endregion SUPPORT METHODS
    }
}
