using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FileSearch
{
    internal class SearchResult
    {
        public string CurrentPath { get; set; }
        public int DirectoriesCount { get; set; } = 0;
        public int ScannedFilesCount { get; set; } = 0;
        public int MatchedFilesCount { get; set; } = 0;
        public TimeSpan ElapsedTime { get; set; }
    }

    internal class FileSearcher
    {
        private readonly TreeView _treeView;

        public BackgroundWorker Worker { get; private set; }
        public DoWorkEventArgs EventArgs { get; private set; }

        public Regex RegEx { get; private set; }
        public FileDirectory Root { get; private set; }
        public SearchResult Result { get; private set; }

        public Stopwatch Stopwatch { get; private set; } = new Stopwatch();
        public bool IsSearchRunning { get; private set; } = false;

        internal class FileDirectory
        {
            public bool IsSearchComplited = false;

            public string DirectoryPath { get; }
            public FileSearcher FileSearcher { get; }
            public FileDirectory Parent { get; }
            public List<FileDirectory> FileDirectories { get; } = new List<FileDirectory>();
            public TreeNode Node { get; }

            public FileDirectory(string path, FileSearcher searcher, FileDirectory parent = null)
            {
                FileSearcher = searcher;
                DirectoryPath = path;
                Parent = parent;
                Node = new TreeNode(Path.GetFileName(DirectoryPath));

                CreateFileDirectories();
            }

            public void Search()
            {
                if (IsSearchComplited)
                {
                    return;
                }

                foreach (var dir in FileDirectories)
                {
                    FileSearcher.Result.DirectoriesCount++;

                    dir.Search();

                    if (FileSearcher.Worker.CancellationPending)
                    {
                        FileSearcher.Stopwatch.Stop();
                        FileSearcher.EventArgs.Cancel = true;

                        return;
                    }
                }

                FileSearcher.Result.CurrentPath = DirectoryPath;

                var fileNodes = GetFileNodes();

                if (fileNodes.Length > 0)
                {
                    if (Equals(FileSearcher.Root))
                    {
                        FileSearcher.UpdateRootNode(fileNodes);
                    }
                    else if (Node.Parent != null)
                    {
                        FileSearcher.UpdateTreeViewNode(Node, fileNodes);
                    }
                    else
                    {
                        Node.Nodes.AddRange(fileNodes);
                        LinkToTreeView();
                    }
                }

                IsSearchComplited = true;
            }

            private void CreateFileDirectories()
            {
                try
                {
                    foreach (var dir in Directory.GetDirectories(DirectoryPath))
                    {
                        FileDirectories.Add(new FileDirectory(dir, FileSearcher, this));
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                    //throw;
                }
            }

            private TreeNode[] GetFileNodes()
            {
                var nodes = new List<TreeNode>();

                try
                {
                    foreach (var file in Directory.GetFiles(DirectoryPath))
                    {
                        var fileName = Path.GetFileName(file);

                        if (FileSearcher.RegEx.IsMatch(fileName))
                        {
                            nodes.Add(new TreeNode(fileName));
                            FileSearcher.Result.MatchedFilesCount++;
                        }

                        FileSearcher.Result.ScannedFilesCount++;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                    //throw;
                }

                return nodes.ToArray();
            }
            
            private void LinkToTreeView()
            {
                var self = this;
                var parent = self.Parent;

                if (parent.Equals(FileSearcher.Root))
                {
                    return;
                }

                while (parent.Node.Parent == null && !parent.Equals(FileSearcher.Root))
                {
                    parent.Node.Nodes.Add(self.Node);
                    self = parent;
                    parent = self.Parent;
                }

                FileSearcher.UpdateTreeViewNode(parent.Node, self.Node);
            }

            public void Reset()
            {
                IsSearchComplited = false;

                FileDirectories.Clear();
                Node.Nodes.Clear();
            }

            public override string ToString()
            {
                return DirectoryPath;
            }
        }

        public FileSearcher(TreeView treeView) 
        {
            _treeView = treeView;
        }

        public void SetWorker(BackgroundWorker worker, DoWorkEventArgs e)
        {
            Worker = worker;
            EventArgs = e;
        }

        public void Search(string path, string pattern)
        {
            if (!IsSearchRunning || path != Root?.DirectoryPath)
            {
                RegEx = new Regex(pattern);
                Reset(path);
            }

            IsSearchRunning = true;

            Stopwatch.Start();
            Root.Search();

            if (Root.IsSearchComplited)
            {
                IsSearchRunning = false;
            }
        }

        private void Reset(string path)
        {
            IsSearchRunning = false;

            Root = new FileDirectory(path, this);
            Result = new SearchResult()
            {
                CurrentPath = Root.DirectoryPath
            };

            Stopwatch.Reset();
            ResetTreeView();
            UpdateRootNode(Root.Node);
        }

        private void ResetTreeView()
        {
            _treeView.BeginInvoke(new Action(() =>
            {
                _treeView.Nodes.Clear();
            })).AsyncWaitHandle.WaitOne();
        }

        private void UpdateRootNode(params TreeNode[] nodes)
        {
            _treeView.BeginInvoke(new Action(() =>
            {
                _treeView.BeginUpdate();
                _treeView.Nodes.AddRange(nodes);
                _treeView.EndUpdate();
            })).AsyncWaitHandle.WaitOne();

            Result.ElapsedTime = Stopwatch.Elapsed;
            Worker.ReportProgress(0, Result);
        }

        private void UpdateTreeViewNode(TreeNode dir, params TreeNode[] nodes)
        {
            // Не знаю как иначе.
            // Без остановки потока слишком часто перерисовывается компонент и с ним невозможно взаимодействовать
            Thread.Sleep(10);

            _treeView.BeginInvoke(new Action(() =>
            {
                _treeView.BeginUpdate();
                dir.Nodes.AddRange(nodes);
                _treeView.EndUpdate();
            })).AsyncWaitHandle.WaitOne();

            Result.ElapsedTime = Stopwatch.Elapsed;
            Worker.ReportProgress(0, Result);
        }
    }
}
