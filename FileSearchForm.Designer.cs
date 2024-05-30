namespace FileSearch
{
    partial class FileSearchForm
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
            this.SearchBtn = new System.Windows.Forms.Button();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.FileRegEx = new System.Windows.Forms.TextBox();
            this.FileRegExLabel = new System.Windows.Forms.Label();
            this.ChangeDirectoryBtn = new System.Windows.Forms.Button();
            this.FileExplorer = new System.Windows.Forms.TreeView();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ScannedFilesCounter = new System.Windows.Forms.Label();
            this.MatchedFilesCounter = new System.Windows.Forms.Label();
            this.ScannedFilesCount = new System.Windows.Forms.Label();
            this.MatchedFilesCount = new System.Windows.Forms.Label();
            this.RootPathLabel = new System.Windows.Forms.Label();
            this.StopSearchBtn = new System.Windows.Forms.Button();
            this.CurrentPathLabel = new System.Windows.Forms.Label();
            this.CurrentPath = new System.Windows.Forms.Label();
            this.DirectoriesCounter = new System.Windows.Forms.Label();
            this.DirectoriesCount = new System.Windows.Forms.Label();
            this.ElapsedTimer = new System.Windows.Forms.Label();
            this.ElapsedTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(33, 74);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(75, 23);
            this.SearchBtn.TabIndex = 0;
            this.SearchBtn.Text = "Start";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // FolderBrowserDialog
            // 
            this.FolderBrowserDialog.Description = "FolderBrowser";
            this.FolderBrowserDialog.SelectedPath = "G:\\Programm Files (x86)\\Steam\\steamapps";
            this.FolderBrowserDialog.ShowNewFolderButton = false;
            // 
            // FileRegEx
            // 
            this.FileRegEx.Location = new System.Drawing.Point(33, 48);
            this.FileRegEx.Name = "FileRegEx";
            this.FileRegEx.Size = new System.Drawing.Size(156, 20);
            this.FileRegEx.TabIndex = 4;
            // 
            // FileRegExLabel
            // 
            this.FileRegExLabel.AutoSize = true;
            this.FileRegExLabel.Location = new System.Drawing.Point(30, 32);
            this.FileRegExLabel.Name = "FileRegExLabel";
            this.FileRegExLabel.Size = new System.Drawing.Size(52, 13);
            this.FileRegExLabel.TabIndex = 5;
            this.FileRegExLabel.Text = "File regex";
            // 
            // ChangeDirectoryBtn
            // 
            this.ChangeDirectoryBtn.Location = new System.Drawing.Point(195, 74);
            this.ChangeDirectoryBtn.Name = "ChangeDirectoryBtn";
            this.ChangeDirectoryBtn.Size = new System.Drawing.Size(75, 23);
            this.ChangeDirectoryBtn.TabIndex = 6;
            this.ChangeDirectoryBtn.Text = "Change Dir";
            this.ChangeDirectoryBtn.UseVisualStyleBackColor = true;
            this.ChangeDirectoryBtn.Click += new System.EventHandler(this.ChangeDirectoryBtn_Click);
            // 
            // FileExplorer
            // 
            this.FileExplorer.AllowDrop = true;
            this.FileExplorer.Location = new System.Drawing.Point(33, 103);
            this.FileExplorer.Name = "FileExplorer";
            this.FileExplorer.Size = new System.Drawing.Size(596, 333);
            this.FileExplorer.TabIndex = 7;
            // 
            // ScannedFilesCounter
            // 
            this.ScannedFilesCounter.AutoSize = true;
            this.ScannedFilesCounter.Location = new System.Drawing.Point(30, 490);
            this.ScannedFilesCounter.Name = "ScannedFilesCounter";
            this.ScannedFilesCounter.Size = new System.Drawing.Size(104, 13);
            this.ScannedFilesCounter.TabIndex = 9;
            this.ScannedFilesCounter.Text = "Scanned files count:";
            // 
            // MatchedFilesCounter
            // 
            this.MatchedFilesCounter.AutoSize = true;
            this.MatchedFilesCounter.Location = new System.Drawing.Point(30, 477);
            this.MatchedFilesCounter.Name = "MatchedFilesCounter";
            this.MatchedFilesCounter.Size = new System.Drawing.Size(75, 13);
            this.MatchedFilesCounter.TabIndex = 10;
            this.MatchedFilesCounter.Text = "Files matched:";
            // 
            // ScannedFilesCount
            // 
            this.ScannedFilesCount.AutoSize = true;
            this.ScannedFilesCount.Location = new System.Drawing.Point(140, 490);
            this.ScannedFilesCount.Name = "ScannedFilesCount";
            this.ScannedFilesCount.Size = new System.Drawing.Size(13, 13);
            this.ScannedFilesCount.TabIndex = 11;
            this.ScannedFilesCount.Text = "0";
            // 
            // MatchedFilesCount
            // 
            this.MatchedFilesCount.AutoSize = true;
            this.MatchedFilesCount.Location = new System.Drawing.Point(140, 477);
            this.MatchedFilesCount.Name = "MatchedFilesCount";
            this.MatchedFilesCount.Size = new System.Drawing.Size(13, 13);
            this.MatchedFilesCount.TabIndex = 12;
            this.MatchedFilesCount.Text = "0";
            // 
            // RootPathLabel
            // 
            this.RootPathLabel.AutoSize = true;
            this.RootPathLabel.Location = new System.Drawing.Point(276, 79);
            this.RootPathLabel.Name = "RootPathLabel";
            this.RootPathLabel.Size = new System.Drawing.Size(211, 13);
            this.RootPathLabel.TabIndex = 13;
            this.RootPathLabel.Text = this.FolderBrowserDialog.SelectedPath;
            // 
            // StopSearchBtn
            // 
            this.StopSearchBtn.Enabled = false;
            this.StopSearchBtn.Location = new System.Drawing.Point(114, 74);
            this.StopSearchBtn.Name = "StopSearchBtn";
            this.StopSearchBtn.Size = new System.Drawing.Size(75, 23);
            this.StopSearchBtn.TabIndex = 14;
            this.StopSearchBtn.Text = "Stop";
            this.StopSearchBtn.UseVisualStyleBackColor = true;
            this.StopSearchBtn.Click += new System.EventHandler(this.StopSearchBtn_Click);
            // 
            // CurrentPathLabel
            // 
            this.CurrentPathLabel.AutoSize = true;
            this.CurrentPathLabel.Location = new System.Drawing.Point(30, 451);
            this.CurrentPathLabel.Name = "CurrentPathLabel";
            this.CurrentPathLabel.Size = new System.Drawing.Size(32, 13);
            this.CurrentPathLabel.TabIndex = 15;
            this.CurrentPathLabel.Text = "Path:";
            // 
            // CurrentPath
            // 
            this.CurrentPath.AutoSize = true;
            this.CurrentPath.Location = new System.Drawing.Point(140, 451);
            this.CurrentPath.Name = "CurrentPath";
            this.CurrentPath.Size = new System.Drawing.Size(211, 13);
            this.CurrentPath.TabIndex = 16;
            this.CurrentPath.Text = this.FolderBrowserDialog.SelectedPath;
            // 
            // DirectoriesCounter
            // 
            this.DirectoriesCounter.AutoSize = true;
            this.DirectoriesCounter.Location = new System.Drawing.Point(30, 464);
            this.DirectoriesCounter.Name = "DirectoriesCounter";
            this.DirectoriesCounter.Size = new System.Drawing.Size(90, 13);
            this.DirectoriesCounter.TabIndex = 17;
            this.DirectoriesCounter.Text = "Directories count:";
            // 
            // DirectoriesCount
            // 
            this.DirectoriesCount.AutoSize = true;
            this.DirectoriesCount.Location = new System.Drawing.Point(140, 464);
            this.DirectoriesCount.Name = "DirectoriesCount";
            this.DirectoriesCount.Size = new System.Drawing.Size(13, 13);
            this.DirectoriesCount.TabIndex = 18;
            this.DirectoriesCount.Text = "0";
            // 
            // ElapsedTimer
            // 
            this.ElapsedTimer.AutoSize = true;
            this.ElapsedTimer.Location = new System.Drawing.Point(30, 510);
            this.ElapsedTimer.Name = "ElapsedTimer";
            this.ElapsedTimer.Size = new System.Drawing.Size(36, 13);
            this.ElapsedTimer.TabIndex = 19;
            this.ElapsedTimer.Text = "Timer:";
            // 
            // ElapsedTime
            // 
            this.ElapsedTime.AutoSize = true;
            this.ElapsedTime.Location = new System.Drawing.Point(140, 510);
            this.ElapsedTime.Name = "ElapsedTime";
            this.ElapsedTime.Size = new System.Drawing.Size(13, 13);
            this.ElapsedTime.TabIndex = 20;
            this.ElapsedTime.Text = "0";
            // 
            // FileSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 532);
            this.Controls.Add(this.ElapsedTime);
            this.Controls.Add(this.ElapsedTimer);
            this.Controls.Add(this.DirectoriesCount);
            this.Controls.Add(this.DirectoriesCounter);
            this.Controls.Add(this.CurrentPath);
            this.Controls.Add(this.CurrentPathLabel);
            this.Controls.Add(this.StopSearchBtn);
            this.Controls.Add(this.RootPathLabel);
            this.Controls.Add(this.MatchedFilesCount);
            this.Controls.Add(this.ScannedFilesCount);
            this.Controls.Add(this.MatchedFilesCounter);
            this.Controls.Add(this.ScannedFilesCounter);
            this.Controls.Add(this.FileExplorer);
            this.Controls.Add(this.ChangeDirectoryBtn);
            this.Controls.Add(this.FileRegExLabel);
            this.Controls.Add(this.FileRegEx);
            this.Controls.Add(this.SearchBtn);
            this.Name = "FileSearchForm";
            this.Text = "FileSearch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.TextBox FileRegEx;
        private System.Windows.Forms.Label FileRegExLabel;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.Button ChangeDirectoryBtn;
        private System.Windows.Forms.TreeView FileExplorer;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private System.Windows.Forms.Label ScannedFilesCounter;
        private System.Windows.Forms.Label MatchedFilesCounter;
        private System.Windows.Forms.Label ScannedFilesCount;
        private System.Windows.Forms.Label MatchedFilesCount;
        private System.Windows.Forms.Label RootPathLabel;
        private System.Windows.Forms.Button StopSearchBtn;
        private System.Windows.Forms.Label CurrentPathLabel;
        private System.Windows.Forms.Label CurrentPath;
        private System.Windows.Forms.Label DirectoriesCounter;
        private System.Windows.Forms.Label DirectoriesCount;
        private System.Windows.Forms.Label ElapsedTimer;
        private System.Windows.Forms.Label ElapsedTime;
    }
}

