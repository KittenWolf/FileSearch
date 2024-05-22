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
            this.DirectoryPathLabel = new System.Windows.Forms.Label();
            this.FileRegEx = new System.Windows.Forms.TextBox();
            this.FileRegExLabel = new System.Windows.Forms.Label();
            this.ChangeDirectoryBtn = new System.Windows.Forms.Button();
            this.FileExplorer = new System.Windows.Forms.TreeView();
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.CancelSearchBtn = new System.Windows.Forms.Button();
            this.ScannedFilesCounter = new System.Windows.Forms.Label();
            this.MatchedFilesCounter = new System.Windows.Forms.Label();
            this.ScannedFilesCount = new System.Windows.Forms.Label();
            this.MatchedFilesCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(25, 140);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(75, 23);
            this.SearchBtn.TabIndex = 0;
            this.SearchBtn.Text = "Search";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // FolderBrowserDialog
            // 
            this.FolderBrowserDialog.Description = "FolderBrowser";
            this.FolderBrowserDialog.SelectedPath = "G:\\Programm Files (x86)\\Steam\\steamapps";
            this.FolderBrowserDialog.ShowNewFolderButton = false;
            // 
            // DirectoryPathLabel
            // 
            this.DirectoryPathLabel.AutoSize = true;
            this.DirectoryPathLabel.Location = new System.Drawing.Point(106, 58);
            this.DirectoryPathLabel.Name = "DirectoryPathLabel";
            this.DirectoryPathLabel.Size = new System.Drawing.Size(211, 13);
            this.DirectoryPathLabel.TabIndex = 3;
            this.DirectoryPathLabel.Text = this.FolderBrowserDialog.SelectedPath;
            // 
            // FileRegEx
            // 
            this.FileRegEx.Location = new System.Drawing.Point(25, 114);
            this.FileRegEx.Name = "FileRegEx";
            this.FileRegEx.Size = new System.Drawing.Size(156, 20);
            this.FileRegEx.TabIndex = 4;
            // 
            // FileRegExLabel
            // 
            this.FileRegExLabel.AutoSize = true;
            this.FileRegExLabel.Location = new System.Drawing.Point(22, 98);
            this.FileRegExLabel.Name = "FileRegExLabel";
            this.FileRegExLabel.Size = new System.Drawing.Size(52, 13);
            this.FileRegExLabel.TabIndex = 5;
            this.FileRegExLabel.Text = "File regex";
            // 
            // ChangeDirectoryBtn
            // 
            this.ChangeDirectoryBtn.Location = new System.Drawing.Point(25, 53);
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
            this.FileExplorer.Location = new System.Drawing.Point(374, 53);
            this.FileExplorer.Name = "FileExplorer";
            this.FileExplorer.Size = new System.Drawing.Size(400, 371);
            this.FileExplorer.TabIndex = 7;
            // 
            // CancelSearchBtn
            // 
            this.CancelSearchBtn.Location = new System.Drawing.Point(106, 140);
            this.CancelSearchBtn.Name = "CancelSearchBtn";
            this.CancelSearchBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelSearchBtn.TabIndex = 8;
            this.CancelSearchBtn.Text = "Cancel";
            this.CancelSearchBtn.UseVisualStyleBackColor = true;
            this.CancelSearchBtn.Click += new System.EventHandler(this.CancelSearchBtn_Click);
            // 
            // ScannedFilesCounter
            // 
            this.ScannedFilesCounter.AutoSize = true;
            this.ScannedFilesCounter.Location = new System.Drawing.Point(371, 462);
            this.ScannedFilesCounter.Name = "ScannedFilesCounter";
            this.ScannedFilesCounter.Size = new System.Drawing.Size(104, 13);
            this.ScannedFilesCounter.TabIndex = 9;
            this.ScannedFilesCounter.Text = "Scanned files count:";
            // 
            // MatchedFilesCounter
            // 
            this.MatchedFilesCounter.AutoSize = true;
            this.MatchedFilesCounter.Location = new System.Drawing.Point(371, 449);
            this.MatchedFilesCounter.Name = "MatchedFilesCounter";
            this.MatchedFilesCounter.Size = new System.Drawing.Size(75, 13);
            this.MatchedFilesCounter.TabIndex = 10;
            this.MatchedFilesCounter.Text = "Files matched:";
            // 
            // ScannedFilesCount
            // 
            this.ScannedFilesCount.AutoSize = true;
            this.ScannedFilesCount.Location = new System.Drawing.Point(481, 462);
            this.ScannedFilesCount.Name = "ScannedFilesCount";
            this.ScannedFilesCount.Size = new System.Drawing.Size(13, 13);
            this.ScannedFilesCount.TabIndex = 11;
            this.ScannedFilesCount.Text = "0";
            // 
            // MatchedFilesCount
            // 
            this.MatchedFilesCount.AutoSize = true;
            this.MatchedFilesCount.Location = new System.Drawing.Point(481, 449);
            this.MatchedFilesCount.Name = "MatchedFilesCount";
            this.MatchedFilesCount.Size = new System.Drawing.Size(13, 13);
            this.MatchedFilesCount.TabIndex = 12;
            this.MatchedFilesCount.Text = "0";
            // 
            // FileSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 499);
            this.Controls.Add(this.MatchedFilesCount);
            this.Controls.Add(this.ScannedFilesCount);
            this.Controls.Add(this.MatchedFilesCounter);
            this.Controls.Add(this.ScannedFilesCounter);
            this.Controls.Add(this.CancelSearchBtn);
            this.Controls.Add(this.FileExplorer);
            this.Controls.Add(this.ChangeDirectoryBtn);
            this.Controls.Add(this.FileRegExLabel);
            this.Controls.Add(this.FileRegEx);
            this.Controls.Add(this.DirectoryPathLabel);
            this.Controls.Add(this.SearchBtn);
            this.Name = "FileSearchForm";
            this.Text = "FileSearch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Label DirectoryPathLabel;
        private System.Windows.Forms.TextBox FileRegEx;
        private System.Windows.Forms.Label FileRegExLabel;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.Button ChangeDirectoryBtn;
        private System.Windows.Forms.TreeView FileExplorer;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private System.Windows.Forms.Button CancelSearchBtn;
        private System.Windows.Forms.Label ScannedFilesCounter;
        private System.Windows.Forms.Label MatchedFilesCounter;
        private System.Windows.Forms.Label ScannedFilesCount;
        private System.Windows.Forms.Label MatchedFilesCount;
    }
}

