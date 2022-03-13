namespace FossyApiDemo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.btnShowInfo = new System.Windows.Forms.Button();
            this.btnCancelUpload = new System.Windows.Forms.Button();
            this.groupUpload = new System.Windows.Forms.GroupBox();
            this.btnBrowseUpload = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.groupFossology = new System.Windows.Forms.GroupBox();
            this.txtFossyUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.rtfLogView = new Tethys.Logging.Controls.RtfLogView();
            this.btnGetToken = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.groupUpload.SuspendLayout();
            this.groupFossology.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.btnGetToken);
            this.splitContainer.Panel1.Controls.Add(this.btnShowInfo);
            this.splitContainer.Panel1.Controls.Add(this.btnCancelUpload);
            this.splitContainer.Panel1.Controls.Add(this.groupUpload);
            this.splitContainer.Panel1.Controls.Add(this.groupFossology);
            this.splitContainer.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer.Panel1.Controls.Add(this.btnUpload);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.rtfLogView);
            this.splitContainer.Size = new System.Drawing.Size(695, 548);
            this.splitContainer.SplitterDistance = 297;
            this.splitContainer.TabIndex = 2;
            // 
            // btnShowInfo
            // 
            this.btnShowInfo.Location = new System.Drawing.Point(153, 245);
            this.btnShowInfo.Name = "btnShowInfo";
            this.btnShowInfo.Size = new System.Drawing.Size(135, 40);
            this.btnShowInfo.TabIndex = 16;
            this.btnShowInfo.Text = "Show Info";
            this.btnShowInfo.UseVisualStyleBackColor = true;
            this.btnShowInfo.Click += new System.EventHandler(this.BtnShowInfoClick);
            // 
            // btnCancelUpload
            // 
            this.btnCancelUpload.Location = new System.Drawing.Point(435, 245);
            this.btnCancelUpload.Name = "btnCancelUpload";
            this.btnCancelUpload.Size = new System.Drawing.Size(135, 40);
            this.btnCancelUpload.TabIndex = 15;
            this.btnCancelUpload.Text = "Cancel Upload";
            this.btnCancelUpload.UseVisualStyleBackColor = true;
            this.btnCancelUpload.Click += new System.EventHandler(this.BtnCancelUploadClick);
            // 
            // groupUpload
            // 
            this.groupUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupUpload.Controls.Add(this.btnBrowseUpload);
            this.groupUpload.Controls.Add(this.txtFolder);
            this.groupUpload.Controls.Add(this.label5);
            this.groupUpload.Controls.Add(this.label3);
            this.groupUpload.Controls.Add(this.txtFile);
            this.groupUpload.Location = new System.Drawing.Point(12, 92);
            this.groupUpload.Name = "groupUpload";
            this.groupUpload.Size = new System.Drawing.Size(671, 74);
            this.groupUpload.TabIndex = 14;
            this.groupUpload.TabStop = false;
            this.groupUpload.Text = " Upload ";
            // 
            // btnBrowseUpload
            // 
            this.btnBrowseUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseUpload.Location = new System.Drawing.Point(639, 43);
            this.btnBrowseUpload.Name = "btnBrowseUpload";
            this.btnBrowseUpload.Size = new System.Drawing.Size(26, 23);
            this.btnBrowseUpload.TabIndex = 12;
            this.btnBrowseUpload.Text = "...";
            this.btnBrowseUpload.UseVisualStyleBackColor = true;
            this.btnBrowseUpload.Click += new System.EventHandler(this.BtnBrowseUploadClick);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(100, 19);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(291, 20);
            this.txtFolder.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Source to scan:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Folder Name:";
            // 
            // txtFile
            // 
            this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile.Location = new System.Drawing.Point(100, 45);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(533, 20);
            this.txtFile.TabIndex = 11;
            // 
            // groupFossology
            // 
            this.groupFossology.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupFossology.Controls.Add(this.txtFossyUrl);
            this.groupFossology.Controls.Add(this.label1);
            this.groupFossology.Controls.Add(this.txtToken);
            this.groupFossology.Controls.Add(this.label2);
            this.groupFossology.Location = new System.Drawing.Point(12, 12);
            this.groupFossology.Name = "groupFossology";
            this.groupFossology.Size = new System.Drawing.Size(671, 74);
            this.groupFossology.TabIndex = 13;
            this.groupFossology.TabStop = false;
            this.groupFossology.Text = " Fossology Settings ";
            // 
            // txtFossyUrl
            // 
            this.txtFossyUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFossyUrl.Location = new System.Drawing.Point(100, 19);
            this.txtFossyUrl.Name = "txtFossyUrl";
            this.txtFossyUrl.Size = new System.Drawing.Size(565, 20);
            this.txtFossyUrl.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "FOSSology URL:";
            // 
            // txtToken
            // 
            this.txtToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToken.Location = new System.Drawing.Point(100, 45);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(565, 20);
            this.txtToken.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Access Token:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Controls.Add(this.lblProgress);
            this.groupBox1.Location = new System.Drawing.Point(12, 172);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(671, 67);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Status ";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(6, 19);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(659, 23);
            this.progressBar.TabIndex = 2;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.Location = new System.Drawing.Point(6, 45);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(659, 19);
            this.lblProgress.TabIndex = 10;
            this.lblProgress.Text = "<...>";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(294, 245);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(135, 40);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.BtnUploadClick);
            // 
            // rtfLogView
            // 
            this.rtfLogView.AddAtTail = true;
            this.rtfLogView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfLogView.LabelText = "Status:";
            this.rtfLogView.Location = new System.Drawing.Point(3, 3);
            this.rtfLogView.MaxLogLength = -1;
            this.rtfLogView.Name = "rtfLogView";
            this.rtfLogView.ShowDebugEvents = false;
            this.rtfLogView.Size = new System.Drawing.Size(689, 241);
            this.rtfLogView.TabIndex = 0;
            this.rtfLogView.TextColor = System.Drawing.Color.Black;
            // 
            // btnGetToken
            // 
            this.btnGetToken.Location = new System.Drawing.Point(12, 245);
            this.btnGetToken.Name = "btnGetToken";
            this.btnGetToken.Size = new System.Drawing.Size(135, 40);
            this.btnGetToken.TabIndex = 17;
            this.btnGetToken.Text = "Get Token";
            this.btnGetToken.UseVisualStyleBackColor = true;
            this.btnGetToken.Click += new System.EventHandler(this.BtnGetTokenClick);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 548);
            this.Controls.Add(this.splitContainer);
            this.Name = "MainForm";
            this.Text = "FOSSology REST API Demo Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainFormDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainFormDragEnter);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.groupUpload.ResumeLayout(false);
            this.groupUpload.PerformLayout();
            this.groupFossology.ResumeLayout(false);
            this.groupFossology.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private Tethys.Logging.Controls.RtfLogView rtfLogView;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFossyUrl;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.GroupBox groupUpload;
        private System.Windows.Forms.GroupBox groupFossology;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowseUpload;
        private System.Windows.Forms.Button btnCancelUpload;
        private System.Windows.Forms.Button btnShowInfo;
        private System.Windows.Forms.Button btnGetToken;
    }
}

