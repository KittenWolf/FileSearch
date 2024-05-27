using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FileSearch
{
    internal class SearchResults
    {
        public LinkedList<TreeNode> ActiveBranch { get; set; }
        public int ScannedFilesCount { get; set; } = 0;
        public int MatchedFilesCount { get; set; } = 0;
    }

    internal class FileSearcher
    {
        private readonly TreeView _treeView;
        private readonly BackgroundWorker _worker;
        private readonly DoWorkEventArgs _e;
        private Regex _regex;        
        private SearchResults _results;
        private LinkedList<TreeNode> _activeBranch;

        public FileSearcher(TreeView treeView, BackgroundWorker worker, DoWorkEventArgs e) 
        {
            _treeView = treeView;
            _worker = worker;
            _e = e;
        }

        public void Search(LinkedList<TreeNode> activeBranch, string pattern)
        {
            _regex = new Regex(pattern);
            _results = new SearchResults();
            _activeBranch = activeBranch;

            DirectorySearch(_activeBranch.Last.Value.Text);
        }

        private bool DirectorySearch(string path)
        {
            if (_worker.CancellationPending)
            {
                _e.Cancel = true;
                return _e.Cancel;
            }

            try
            {
                foreach (var dir in Directory.GetDirectories(path))
                {
                    var branch = new TreeNode(dir);

                    _activeBranch.AddLast(branch);

                    var cancel = DirectorySearch(dir);

                    if (cancel)
                    {
                        return cancel;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            var fileNodes = GetFileNodes(path);
            
            if (fileNodes.Length > 0)
            {
                LinkBranchToRoot();
                UpdateTreeNode(_activeBranch.Last.Value, fileNodes);
            }

            _activeBranch.RemoveLast();
            _results.ActiveBranch = _activeBranch;

            return _e.Cancel;
        }

        private TreeNode[] GetFileNodes(string path)
        {
            var nodes = new List<TreeNode>();

            try
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    var fileName = Path.GetFileName(file);

                    _results.ScannedFilesCount++;

                    if (_regex.IsMatch(fileName))
                    {
                        nodes.Add(new TreeNode(fileName));
                        _results.MatchedFilesCount++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return nodes.ToArray();
        }

        private void LinkBranchToRoot()
        {
            var last = _activeBranch.Last;
            var root = _activeBranch.First;

            if (last.Value == root.Value)
            {
                return;
            }

            while (last.Previous.Value.Parent == null && last.Previous.Value != root.Value) 
            {
                last.Previous.Value.Nodes.Add(last.Value);

                last = last.Previous;
            }

            UpdateTreeNode(last.Previous.Value, last.Value);
        }

        private void UpdateTreeNode(TreeNode branch, params TreeNode[] values)
        {
            Thread.Sleep(10);

            _treeView.BeginInvoke(new Action(() =>
            {
                _treeView.BeginUpdate();

                foreach (var node in values)
                {
                    if (!branch.Nodes.Contains(node))
                    {
                        branch.Nodes.Add(node);
                    }
                }

                _treeView.EndUpdate();
            })).AsyncWaitHandle.WaitOne();

            _worker.ReportProgress(0, _results);
        }
    }
}
