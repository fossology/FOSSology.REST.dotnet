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

namespace Fossology.Rest.Dotnet.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Threading;
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
        private const string Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE1OTM2NDc5OTksIm5iZiI6MTU5MzA0MzIwMCwianRpIjoiTWk0eiIsInNjb3BlIjoid3JpdGUifQ.YsZLPym6rRUdBsEtEderJS2Xlj09DOG0J3z0Ygbv8MI";

        /// <summary>
        /// The filename of a test package.
        /// </summary>
        private const string Filename = @"..\..\..\..\TestData\fetch-retry-master.zip";

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
            request.TokenName = "TestToken1";
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
            var result = client.UploadPackage(Filename, 3);
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
        public void TestUploadPackageAndCheckLookAt()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.UploadPackage(Filename, 3);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            var uploadId = int.Parse(result.Message);
            Debug.WriteLine($"Upload id = {uploadId}");

            try
            {
                var result2 = client.GetUpload(uploadId);
                Assert.IsNotNull(result2);
                Assert.AreEqual(uploadId, result2.Id);
                Debug.WriteLine($"Upload = {result}");
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
            var client = new FossologyClient(LocalUrl, Token);
            var result = client.UploadPackage(@"..\..\..\TestData\xtxgd.zip", 5);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            var uploadId = int.Parse(result.Message);
            Debug.WriteLine($"Upload id = {uploadId}");

            WaitUntilUploadIsDone(client, uploadId);
            Debug.WriteLine($"Upload id = {uploadId} is now available");
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestUploadPackageFromUrl()
        {
            var client = new FossologyClient(LocalUrl, Token);
            var details = new UrlUpload();
            details.Name = "Tethys.xml_v1.0.0.zip";
            details.Url = "https://github.com/tngraf/Tethys.Xml/archive/v1.0.0.zip";
            details.MaxRecursionDepth = 0;

            var result = client.UploadPackageFromUrl(3, details);
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
            var client = new FossologyClient(LocalUrl, Token);
            var details = new VcsUpload();
            details.VcsName = "Tethys.Logging";
            details.VcsUrl = "https://github.com/tngraf/Tethys.Logging.git";
            details.VcsBranch = "master";
            details.VcsType = "git";
            details.VcsUsername = "xxx";
            details.VcsPassword = "xxx";

            var result = client.UploadPackageFromVcs(3, details);
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
            const int Id = 2;

            var client = new FossologyClient(LocalUrl, Token);
            var summary = client.GetUploadSummary(Id);
            Assert.IsNotNull(summary);
            Assert.AreEqual(Id, summary.Id);
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
            Assert.IsTrue(result.Count > 0);
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
            const int Id = 2;

            var client = new FossologyClient(LocalUrl, Token);
            var licenses = client.GetUploadLicenses(Id, "nomos", true);
            Assert.IsNotNull(licenses);
            foreach (var info in licenses)
            {
                Debug.WriteLine($"Upload licenses found by nomos = {info}");
            } // foreach

            licenses = client.GetUploadLicenses(Id, "monk", true);
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
            job.Decider.NewScanner = true;
            job.Decider.OjoDecider = true;
            job.Reuse.ReuseUploadId = 0;
            job.Reuse.ReuseGroup = 0;
            job.Reuse.ReuseMain = true;
            job.Reuse.ReuseEnhanced = true;
            var result = client.TriggerJob(3, 2, job, "fossy");
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
            client.DownloadReport(7, ReportName);
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
            var folderlist = client.GetFolderList();
            Assert.IsNotNull(folderlist);
            if ((folderlist.Count != 1) && (folderlist.Count != 2))
            {
                Assert.Fail("Invalid number of folders");
            } // if

            var folder = client.GetFolder(1);
            Assert.IsNotNull(folder);
            Assert.AreEqual(1, folder.Id);

            Result result;
            var folderId = FindFolder(folderlist, TestFolderName);
            if (folderId < 0)
            {
                result = client.CreateFolder(TestFolderName, 1);
                Assert.IsNotNull(result);
                Assert.AreEqual("INFO", result.Type);
                Assert.AreEqual(201, result.Code);
                folderId = int.Parse(result.Message);
                Assert.IsTrue(folderId > 0);
            } // if

            result = client.UploadPackage(Filename, folderId);
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            var uploadId = int.Parse(result.Message);
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
            result = client.TriggerJob(folderId, uploadId, jobTrigger);
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
            Assert.IsTrue(licensesFound.Count > 0);
            Assert.AreEqual("MIT", licensesFound[0].AgentFindings[0]);

            result = client.TriggerReportGeneration(uploadId, "spdx2");
            Assert.IsNotNull(result);
            Assert.AreEqual("INFO", result.Type);
            Assert.AreEqual(201, result.Code);
            // extract report id
            var text = result.Message.Substring(result.Message.LastIndexOf('/') + 1);
            var reportId = int.Parse(text);

            if (File.Exists(ReportFilename))
            {
                File.Delete(ReportFilename);
            } // if

            // ugly but required: wait some time until report is available
            Thread.Sleep(3000);

            client.DownloadReport(reportId, ReportFilename);
            Assert.IsTrue(File.Exists(ReportFilename));

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
    }
}
