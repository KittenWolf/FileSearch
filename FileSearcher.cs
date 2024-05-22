using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FileSearch
{
    internal class SearchResults
    {
        public int ScannedFilesCount { get; set; } = 0;
        public int MatchedFilesCount { get; set; } = 0;
    }

    internal class FileSearcher
    {
        private readonly TreeView _treeView;
        private readonly BackgroundWorker _worker;
        private TreeNode _root;
        private Regex _regex;        
        private SearchResults _results;

        public FileSearcher(TreeView treeView, BackgroundWorker worker) 
        {
            _treeView = treeView;
            _worker = worker;
        }

        public void Search(string path, string pattern)
        {
            _regex = new Regex(pattern);
            _results = new SearchResults();

            CreateTreeViewRoot(path);
            SearchByBranch(_root);
        }

        private void CreateTreeViewRoot(string path)
        {
            _root = new TreeNode(path);

            _treeView.BeginInvoke(new Action(() =>
            {
                _treeView.Nodes.Add(_root);
            }));
        }

        private bool SearchByBranch(TreeNode branch)
        {
            var result = false;
            var fileNodes = GetFileNodes(branch);

            try
            {
                foreach (var dir in Directory.GetDirectories(branch.Text))
                {
                    var newBranch = new TreeNode(dir);
                    var tempResult = SearchByBranch(newBranch);

                    if (tempResult)
                    {
                        UpdateTreeNode(newBranch, branch);
                        _worker.ReportProgress(0, _results);
                    }

                    result |= tempResult;
                }
            }
            catch (Exception)
            {
                throw;
            }

            foreach (var file in fileNodes)
            {
                UpdateTreeNode(file, branch);
                result = true;
            }

            return result;            
        }

        private List<TreeNode> GetFileNodes(TreeNode parent)
        {
            List<TreeNode> result = new List<TreeNode>();

            try
            {
                foreach (var file in Directory.GetFiles(parent.Text))
                {
                    _results.ScannedFilesCount++;
                    var fileName = Path.GetFileName(file);

                    if (_regex.IsMatch(fileName))
                    {
                        _results.MatchedFilesCount++;
                        result.Add(new TreeNode(fileName));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }   
            
            return result;
        }

        private void UpdateTreeNode(TreeNode node, TreeNode branch)
        {
            node.Text = Path.GetFileName(node.Text);

            _treeView.BeginInvoke(new Action(() =>
            {
                _treeView.BeginUpdate();
                branch.Nodes.Add(node);
                _treeView.EndUpdate();
            }));
        }
    }
}
