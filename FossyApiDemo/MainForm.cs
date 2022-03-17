// ---------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Tethys">
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

namespace FossyApiDemo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Fossology.Rest.Dotnet;
    using Fossology.Rest.Dotnet.Model;

    using Tethys.Logging;

    /// <summary>
    /// Main form of the application.
    /// </summary>
    /// <seealso cref="Form" />
    public partial class MainForm : Form
    {
        /// <summary>
        /// Delegate to set status.
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <param name="text">The text.</param>
        private delegate void SetStatusDelegate(int progress, string text);

        #region PRIVATE PROPERTIES
        /// <summary>
        /// The logger for this class.
        /// </summary>
        private static ILog log;

        /// <summary>
        /// The Fossology client.
        /// </summary>
        private FossologyClient client;

        /// <summary>
        /// The folder identifier.
        /// </summary>
        private int folderId;

        /// <summary>
        /// The upload identifier.
        /// </summary>
        private int uploadId;

        /// <summary>
        /// The job identifier.
        /// </summary>
        private int jobId;

        /// <summary>
        /// The status.
        /// </summary>
        private Job status;

        /// <summary>
        /// The report identifier.
        /// </summary>
        private int reportId;

        /// <summary>
        /// The 'cancel upload' flag.
        /// </summary>
        private bool cancelUpload;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.ConfigureLogging();

            this.client = null;
            this.folderId = -1;
            this.uploadId = -1;
            this.jobId = -1;
        } // MainForm()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region UI HANDLING
        /// <summary>
        /// Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing
        /// the event data.</param>
        private void MainFormLoad(object sender, EventArgs e)
        {
#if DEBUG
            this.txtFossyUrl.Text = "http://localhost:8081/repo/api/v1";
            this.txtToken.Text = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2NDc0NzUxOTksIm5iZiI6MTY0NzEyOTYwMCwianRpIjoiTXk0eiIsInNjb3BlIjoid3JpdGUifQ.DSDQDyodi5LgIUyqUbOKQiMtpoqOkI0RnF-uphwI_0A";
            this.txtFolder.Text = "TestFolder";
            this.txtFile.Text = @"..\..\..\TestData\fetch-retry-master.zip";
#endif
            this.InitializeStatusDisplay();
            log.Info("Ready.");
        } // MainFormLoad()

        /// <summary>
        /// Handles the FormClosing event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance
        /// containing the event data.</param>
        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
        } // MainFormFormClosing()

        /// <summary>
        /// Handles the Click event of the <c>btnBrowse</c> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnBrowseUploadClick(object sender, EventArgs e)
        {
            this.OpenFile();
        } // BtnBrowseUploadClick()

        /// <summary>
        /// Handles the Click event of the btnGetToken control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnGetTokenClick(object sender, EventArgs e)
        {
            try
            {
                log.Info("Creating token for default user 'fossy'...");
                var token = this.GetToken();

                log.Info($"Token = {token}");

                this.txtToken.Text = token;
                log.Info("Token has been inserted into token text box.");

                // clear existing client, because it has been initialized with an old token
                this.client = null;
            }
            catch (Exception ex)
            {
                log.Error("Error getting token: " + ex.Message);
            } // if
        } // BtnGetTokenClick()

        /// <summary>
        /// Handles the Click event of the btnShowInfo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnShowInfoClick(object sender, EventArgs e)
        {
            try
            {
                this.ShowInfo();
            }
            catch (Exception ex)
            {
                log.Error("Error showing info: " + ex.Message);
            } // if
        } // BtnShowInfoClick()

        /// <summary>
        /// Handles the Click event of the <c>btnUpload</c> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnUploadClick(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckFolder())
                {
                    return;
                } // if

                if (string.IsNullOrEmpty(this.txtFile.Text))
                {
                    log.Error("No file to upload specified!");
                } // if

                this.cancelUpload = false;
                var result = await this.ProcessFileAsync(this.txtFile.Text);
                if (result)
                {
                    log.Info("Done.");
                } // if
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex.Message);
            } // if
        } // BtnUploadClick()

        /// <summary>
        /// Handles the Click event of the <c>btnCancelUpload</c> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnCancelUploadClick(object sender, EventArgs e)
        {
            this.cancelUpload = true;
        } // BtnCancelUploadClick()

        /// <summary>
        /// Handles the DragEnter event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragEventArgs"/> instance
        /// containing the event data.</param>
        private void MainFormDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy : DragDropEffects.None;
        } // MainFormDragEnter()

        /// <summary>
        /// Handles the DragDrop event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DragEventArgs"/> instance
        /// containing the event data.</param>
        private void MainFormDragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            } // if

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            this.txtFile.Text = files[0];
        } // MainFormDragDrop()
        #endregion // UI HANDLING

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Gets an access token for the default user 'fossy'.
        /// </summary>
        /// <returns>System.String.</returns>
        private string GetToken()
        {
            if (this.client == null)
            {
                this.CreateClient();
            } // if

            var request = new TokenRequest();
            request.Username = "fossy";
            request.Password = "fossy";
            request.TokenName = "TestToken";
            request.TokenScope = "write";
            request.TokenExpire = DateTime.Today.AddDays(3).ToString("yyyy-MM-dd");
            var result = this.client.GetToken(request);

            return result;
        } // GetToken()

        /// <summary>
        /// Shows the information.
        /// </summary>
        private void ShowInfo()
        {
            if (this.client == null)
            {
                this.CreateClient();
            } // if

            // get version does not require an access token
            var version = this.client.GetVersion();
            log.Info($"FOSSology version = {version.Version}");

            // get user does require an access token
            var users = this.client.GetUserList();
            foreach (var user in users)
            {
                if (user.Name != "fossy")
                {
                    continue;
                } // if

                var userinfo = this.client.GetUser(user.Id);
                log.Info($"Current user = {userinfo.Name}, email = {userinfo.Email}, description = {userinfo.Description}");
            } // foreach
        } // ShowInfo()

        /// <summary>
        /// Processes the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// <c>true</c> if the upload has been processed successfully; otherwise <c>false</c>.
        /// </returns>
        private async Task<bool> ProcessFileAsync(string fileName)
        {
            return await Task.Run(() => this.ProcessFile(fileName));
        } // ProcessFileAsync()

        /// <summary>
        /// Starts the jobs.
        /// </summary>
        /// <returns>The job id.</returns>
        private async Task<int> StartJob()
        {
            this.SetStatus(5, "Starting analysis jobs...");

            while (true)
            {
                try
                {
                    if (this.cancelUpload)
                    {
                        return -1;
                    } // if

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
                    var result = this.client.TriggerJob(this.folderId, this.uploadId, jobTrigger);
                    if (result == null)
                    {
                        LogErrorMessage("Unable to start analysis jobs", null);
                        return -1;
                    } // if

                    if ((result.Type == "INFO") || (result.Code == 201))
                    {
                        var id = int.Parse(result.Message);
                        return id;
                    } // if
                }
                catch (FossologyApiException fex)
                {
                    if (fex.HttpStatusCode != HttpStatusCode.NotFound)
                    {
                        log.Error("Error starting jobs: ", fex);
                        return -1;
                    } // if

                    // if the file is not found, we assume that that upload has not yet finished
                    this.SetStatus(10, "File not yet available - waiting for upload to finish and files to get unpacked...");
                    log.Debug("File not yet available - waiting for upload to finish and files to get unpacked...");
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    log.Error("Error starting jobs: ", ex);
                    return -1;
                }
            } // while
        } // StartJob()

        /// <summary>
        /// Processes the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// <c>true</c> if the upload has been processed successfully; otherwise <c>false</c>.
        /// </returns>
        private async Task<bool> ProcessFile(string fileName)
        {
            try
            {
                this.SetStatus(5, "Uploading package");
                var uploadFinished = false;
                var result = this.client.UploadPackage(
                    fileName, this.folderId, string.Empty, () => uploadFinished = true);
                if ((result == null) || (result.Type != "INFO") || (result.Code != 201))
                {
                    LogErrorMessage("Unable to upload package", result);
                    return false;
                } // if

                this.uploadId = int.Parse(result.Message);

                if (this.cancelUpload)
                {
                    return false;
                } // if

                while (!uploadFinished)
                {
                    this.SetStatus(10, "Waiting for upload to finish...");
                    log.Debug("File not yet available - waiting for upload to finish...");
                    await Task.Delay(1000);
                } // while

                log.Debug("Upload finished.");

                // allow some tim to start (internal) unpack job
                await Task.Delay(1000);

                if (this.cancelUpload)
                {
                    return false;
                } // if

                var currentJobId = await this.StartJob();
                if (currentJobId < 0)
                {
                    return false;
                }

                this.jobId = currentJobId;

                if (!this.WaitForJobCompletion())
                {
                    log.Error("Error waiting for job completion!");
                    return false;
                } // if

                this.SetStatus(80, "Starting report generation...");
                result = this.client.TriggerReportGeneration(this.uploadId, "spdx2");
                if ((result == null) || (result.Type != "INFO") || (result.Code != 201))
                {
                    LogErrorMessage("Unable to trigger report generation", result);
                    return false;
                } // if

                if (this.cancelUpload)
                {
                    return false;
                } // if

                // extra check for report id
                var index = result.Message.LastIndexOf('/');
                this.reportId = int.Parse(index >= 0
                   ? result.Message.Substring(index + 1) : result.Message);

                var upload = this.client.GetUpload(this.uploadId);
                if (upload == null)
                {
                    log.Error("Error retrieving upload!");
                    return false;
                } // if

                if (this.cancelUpload)
                {
                    return false;
                } // if

                var reportFilename = upload.UploadName + ".spdx2.rdf.xml";
                if (System.IO.File.Exists(reportFilename))
                {
                    System.IO.File.Delete(reportFilename);
                } // if

                this.SetStatus(90, "Downloading report...");
                if (!await this.DownloadReport(reportFilename))
                {
                    log.Error("Error downloading report!");
                } // if

                this.SetStatus(100, "Done.");
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error processing file: ", ex);
                return false;
            } // catch
        } // ProcessFile()

        /// <summary>
        /// Downloads the report.
        /// </summary>
        /// <param name="reportFilename">Name of the report.</param>
        /// <returns>
        /// <c>true</c> if the report has been processed downloaded; otherwise <c>false</c>.
        /// </returns>
        private async Task<bool> DownloadReport(string reportFilename)
        {
            while (true)
            {
                try
                {
                    this.client.DownloadReport(this.reportId, reportFilename);
                    break;
                }
                catch (FossologyApiException fex)
                {
                    if (fex.HttpStatusCode == HttpStatusCode.ServiceUnavailable)
                    {
                        // report not yet ready
                        await Task.Delay(1000);
                    } // if
                }
                catch (Exception ex)
                {
                    log.Error("Error downloading report ", ex);
                } // catch
            } // while

            if (!System.IO.File.Exists(reportFilename))
            {
                log.Error("Error downloading report!");
                return false;
            } // if

            return true;
        } // DownloadReport()

        /// <summary>
        /// Waits for job completion.
        /// </summary>
        /// <returns><c>true</c> if the job has been successfully completed; otherwise <c>false</c>.</returns>
        private bool WaitForJobCompletion()
        {
            while (true)
            {
                if (this.cancelUpload)
                {
                    return false;
                } // if

                this.status = this.client.GetJob(this.jobId);
                if (this.status == null)
                {
                    return false;
                } // if

                this.jobId = this.status.Id;
                var text = $"Waiting for jobs to end, status = '{this.status.Status}'";
                if (this.status.Eta > 0)
                {
                    text += $", time to finish = {SecondsToTime(this.status.Eta)}";
                } // if

                this.SetStatus(50, text);
                log.Debug($"Job status: eta = {this.status.Eta}, status = '{this.status.Status}'");
                if (this.status.Status == "Completed")
                {
                    return true;
                } // if

                if (this.status.Status == "Failed")
                {
                    return false;
                } // if

                var milliSecondsToWait = 1000;
                if (this.status.Eta > 300)
                {
                    milliSecondsToWait = 30 * 1000;
                }
                else if (this.status.Eta > 20)
                {
                    milliSecondsToWait = 10 * 1000;
                } // if

                Thread.Sleep(milliSecondsToWait);
            } // while
        } // WaitForJobCompletion()

        /// <summary>
        /// Checks the folder.
        /// </summary>
        /// <returns><c>true</c> if the folder was found; otherwise <c>false</c>.</returns>
        private bool CheckFolder()
        {
            if (this.client == null)
            {
                this.CreateClient();
            } // if

            if (string.IsNullOrEmpty(this.txtFolder.Text))
            {
                log.Error("No Fossology folder specified!");
                return false;
            } // if

            try
            {
                // verify access by reading folder data
                var folderList = this.client.GetFolderList();
                log.Debug("Retrieving folder list:");
                foreach (var folder in folderList)
                {
                    log.Debug($"  Folder = {folder}");
                    if (folder.Name.Equals(this.txtFolder.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        this.folderId = folder.Id;
                    } // if
                } // foreach

                if (this.folderId >= 0)
                {
                    log.Debug($"Folder has id {this.folderId}");
                    return true;
                }
                else
                {
                    log.Error($"Folder '{this.txtFolder.Name}' was not found!");
                } // if
            }
            catch (Exception ex)
            {
                log.Error("Error accessing Fossology folder: ", ex);
            } // catch

            return false;
        } // CheckFolder()

        /// <summary>
        /// Logs the given error message.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="result">The result.</param>
        private static void LogErrorMessage(string text, Result result)
        {
            log.Error(result == null ? $"{text}" : $"{text}: {result.Message}");
        } // LogErrorMessage()

        /// <summary>
        /// Converts a total number of seconds to a more user friendly time string.
        /// </summary>
        /// <param name="totalSeconds">The total seconds.</param>
        /// <returns>A user friendly time string.</returns>
        private static string SecondsToTime(int totalSeconds)
        {
            var ts = TimeSpan.FromSeconds(totalSeconds);

            if (ts.Hours > 0)
            {
                return ts.ToString(@"hh\:mm\:ss");
            } // if

            if (ts.Minutes > 0)
            {
                return ts.ToString(@"mm\:ss");
            } // if

            return ts.ToString(@"ss");
        } // SecondsToTime()

        /// <summary>
        /// Sets the status.
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <param name="text">The text.</param>
        private void SetStatus(int progress, string text)
        {
            if (!this.InvokeRequired)
            {
                if (progress >= 0)
                {
                    if (this.progressBar.Value != progress)
                    {
                        this.progressBar.Value = progress;
                    } // if
                } // if

                if (!string.IsNullOrEmpty(text))
                {
                    this.lblProgress.Text = text;
                } // if
            }
            else
            {
                this.BeginInvoke(new SetStatusDelegate(this.SetStatus), progress, text);
            } // if
        } // SetStatus()

        /// <summary>
        /// Creates the Fossology client.
        /// </summary>
        private void CreateClient()
        {
            if (string.IsNullOrEmpty(this.txtFossyUrl.Text))
            {
                log.Error("No Fossology URL specified!");
                return;
            } // if

            if (string.IsNullOrEmpty(this.txtToken.Text))
            {
                log.Error("No token specified!");
                return;
            } // if

            this.client = new FossologyClient(this.txtFossyUrl.Text, this.txtToken.Text);
        } // CreateClient()

        /// <summary>
        /// Opens a file.
        /// </summary>
        private void OpenFile()
        {
            using (var dlg = new OpenFileDialog
                 {
                     InitialDirectory = ".",
                     RestoreDirectory = true,
                     Filter = @"All Files (*.*) |*.*||",
                 })
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                } // if

                this.txtFile.Text = dlg.FileName;
            } // using
        } // OpenFile()

        /// <summary>
        /// Initializes the status display.
        /// </summary>
        private void InitializeStatusDisplay()
        {
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = 100;
            this.progressBar.Value = 0;
            this.progressBar.MarqueeAnimationSpeed = 0;

            this.lblProgress.Text = string.Empty;
        } // InitializeStatusDisplay()

        /// <summary>
        /// Configures the logging.
        /// </summary>
        private void ConfigureLogging()
        {
            this.rtfLogView.AddAtTail = true;
#if !DEBUG
            this.rtfLogView.MaxLogLength = 10000;
#endif
            this.rtfLogView.ShowDebugEvents = true;

            var settings = new Dictionary<string, string>();
            ////settings.Add("AddTime", "false");
            ////settings.Add("AddLevel", "false");
            LogManager.Adapter = new LogViewFactoryAdapter(this.rtfLogView, settings);
            log = LogManager.GetLogger(typeof(MainForm));
        } // ConfigureLogging()
        #endregion // PRIVATE METHODS
    } // MainForm
}
