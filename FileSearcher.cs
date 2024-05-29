using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FileSearch
{
    internal class SearchResult
    {
        public int ScannedFilesCount { get; set; } = 0;
        public int MatchedFilesCount { get; set; } = 0;
        public int DirectoriesCount { get; set; } = 0;
    }

    internal class FileSearcher
    {
        private readonly TreeView _treeView;

        public BackgroundWorker Worker { get; private set; }
        public DoWorkEventArgs EventArgs { get; private set; }

        public Regex RegEx { get; private set; }
        public FileDirectory Root { get; private set; }
        public SearchResult Result { get; private set; }

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
                if (FileSearcher.Worker.CancellationPending)
                {
                    FileSearcher.EventArgs.Cancel = true;
                    return;
                }

                if (IsSearchComplited)
                {
                    return;
                }

                foreach (var dir in FileDirectories)
                {
                    dir.Search();

                    if (FileSearcher.Worker.CancellationPending)
                    {
                        return;
                    }
                }

                var fileNodes = GetFileNodes();

                if (fileNodes.Length > 0)
                {
                    if (Node.Parent != null)
                    {
                        FileSearcher.UpdateTreeViewNode(Node, fileNodes);
                    }
                    else if (Equals(FileSearcher.Root))
                    {
                        FileSearcher.UpdateRootNode(fileNodes);
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
                    throw;
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
                    throw;
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

            public override string ToString()
            {
                return DirectoryPath;
            }
        }

        public FileSearcher(TreeView treeView) 
        {
            _treeView = treeView;
        }

        public void SetRoot(string path)
        {
            Result = new SearchResult();
            Root = new FileDirectory(path, this);
            _treeView.Nodes.Clear();
            _treeView.Nodes.Add(Root.Node);
        }

        public void SetWorker(BackgroundWorker worker, DoWorkEventArgs e)
        {
            Worker = worker;
            EventArgs = e;
        }

        public void Search(string pattern)
        {
            RegEx = new Regex(pattern);

            Root.Search();
        }

        private void UpdateRootNode(params TreeNode[] nodes)
        {
            _treeView.BeginInvoke(new Action(() =>
            {
                _treeView.BeginUpdate();
                _treeView.Nodes[0].Nodes.AddRange(nodes);
                _treeView.EndUpdate();
            })).AsyncWaitHandle.WaitOne();

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

            Worker.ReportProgress(0, Result);
        }
    }
}
