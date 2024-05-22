using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FileSearch
{
    public partial class FileSearchForm : Form
    {
        public FileSearchForm()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
        }

        private void InitializeBackgroundWorker()
        {
            BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            BackgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var results = e.UserState as SearchResults;

            ScannedFilesCount.Text = results.ScannedFilesCount.ToString();
            MatchedFilesCount.Text = results.MatchedFilesCount.ToString();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SearchBtn.Enabled = true;
            CancelSearchBtn.Enabled = false;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            var fileSearcher = new FileSearcher(FileExplorer, worker);
            var path = FolderBrowserDialog.SelectedPath;
            var pattern = FileRegEx.Text.Trim();
            
            fileSearcher.Search(path, pattern);
        }

        private void ChangeDirectoryBtn_Click(object sender, System.EventArgs e)
        {
            if (FolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                DirectoryPathLabel.Text = FolderBrowserDialog.SelectedPath;
            }
        }        

        private void SearchBtn_Click(object sender, System.EventArgs e)
        {
            FileExplorer.Nodes.Clear();

            SearchBtn.Enabled = false;
            CancelSearchBtn.Enabled = true;

            if (!BackgroundWorker.IsBusy)
            {
                BackgroundWorker.RunWorkerAsync();
            }
        }

        private void CancelSearchBtn_Click(object sender, System.EventArgs e)
        {
            BackgroundWorker.CancelAsync();

            SearchBtn.Enabled = true;
            CancelSearchBtn.Enabled = false;
        }
    }
}
