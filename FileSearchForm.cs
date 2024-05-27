using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace FileSearch
{
    public partial class FileSearchForm : Form
    {
        private LinkedList<TreeNode> ActiveBranch = new LinkedList<TreeNode>();

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
            CurrentDirPath.Text = results.ActiveBranch?.Last?.Value.Text.ToString();

            ActiveBranch = results.ActiveBranch;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Console.WriteLine("Background operation was canceled.");
            } 

            SearchBtn.Text = "Start";
            ActiveBranch = new LinkedList<TreeNode>();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            var fileSearcher = new FileSearcher(FileExplorer, worker, e);
            var pattern = FileRegEx.Text.Trim();
            
            fileSearcher.Search(ActiveBranch, pattern);
        }

        private void ChangeDirectoryBtn_Click(object sender, EventArgs e)
        {
            if (FolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                SetRootNode(FolderBrowserDialog.SelectedPath);
            }
        }        

        private void SetRootNode(string path)
        {
            FileExplorer.Nodes.Clear();
            ActiveBranch.Clear();

            var root = new TreeNode(path);

            ActiveBranch.AddFirst(root);
            FileExplorer.Nodes.Add(root);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (!BackgroundWorker.IsBusy)
            {
                SetRootNode(FolderBrowserDialog.SelectedPath);
                BackgroundWorker.RunWorkerAsync();
                SearchBtn.Text = "Stop";
            } 
            else
            {
                BackgroundWorker.CancelAsync();
                SearchBtn.Text = "Start";
            }
        }
    }
}
