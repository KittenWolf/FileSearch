using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace FileSearch
{
    public partial class FileSearchForm : Form
    {
        private readonly FileSearcher _fileSearcher;

        public FileSearchForm()
        {
            InitializeComponent();
            InitializeBackgroundWorker();

            _fileSearcher = new FileSearcher(FileExplorer);
        }

        private void InitializeBackgroundWorker()
        {
            BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            BackgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var results = e.UserState as SearchResult;

            CurrentPath.Text = results.CurrentPath;
            DirectoriesCount.Text = results.DirectoriesCount.ToString();
            ScannedFilesCount.Text = results.ScannedFilesCount.ToString();
            MatchedFilesCount.Text = results.MatchedFilesCount.ToString();
            ElapsedTime.Text = results.ElapsedTime.ToString();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SearchBtn.Enabled = true;
            StopSearchBtn.Enabled = false;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var pattern = FileRegEx.Text.Trim();

            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            
            _fileSearcher.SetWorker(worker, e);
            _fileSearcher.Search(FolderBrowserDialog.SelectedPath, pattern);
        }

        private void ChangeDirectoryBtn_Click(object sender, EventArgs e)
        {
            if (FolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                RootPathLabel.Text = FolderBrowserDialog.SelectedPath;
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            BackgroundWorker.RunWorkerAsync();
            StopSearchBtn.Enabled = true; 
            SearchBtn.Enabled = false;
        }

        private void StopSearchBtn_Click(object sender, EventArgs e)
        {
            BackgroundWorker.CancelAsync();
            StopSearchBtn.Enabled = false; 
            SearchBtn.Enabled = true;
        }
    }
}
