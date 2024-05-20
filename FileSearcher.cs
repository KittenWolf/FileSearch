using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FileSearch
{
    internal class FileSearcher
    {
        private readonly TreeView _treeView;
        private readonly Label _scannedFiles;
        private readonly Label _matchedFiles;
        private readonly BackgroundWorker _worker;
        private Regex _regex;
        private TreeNode _root;
        private int _scannedFilesCount = 0;
        private int _matchedFilesCount = 0;

        public FileSearcher(TreeView treeView, Label scannedFiles, Label matchedFiles, BackgroundWorker worker) 
        {
            _treeView = treeView;
            _scannedFiles = scannedFiles;
            _matchedFiles = matchedFiles;
            _worker = worker;
        }

        public void Search(string path, string pattern)
        {
            _scannedFilesCount = 0;
            _matchedFilesCount = 0;

            _root = new TreeNode(path);
            _regex = new Regex(pattern);

            _treeView.Nodes.Add(_root);

            SearchByBranch(_root);
        }

        public bool SearchByBranch(TreeNode branch)
        {
            bool result = false;

            try
            {
                foreach (var dir in Directory.GetDirectories(branch.Text))
                {
                    var newBranch = new TreeNode(dir);

                    branch.Nodes.Add(newBranch);
                    var tempResult = SearchByBranch(newBranch);

                    if (!tempResult)
                    {
                        branch.Nodes.Remove(newBranch);
                    }

                    newBranch.Text = Path.GetFileName(dir);
                    result |= tempResult;
                }

                foreach (var file in Directory.GetFiles(branch.Text))
                {
                    var fileName = Path.GetFileName(file);

                    _scannedFilesCount++;

                    if (_regex.IsMatch(fileName))
                    {
                        UpdateTreeView(branch, fileName);

                        _matchedFilesCount++;

                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
            }

            _scannedFiles.Text = _scannedFilesCount.ToString();
            _matchedFiles.Text = _matchedFilesCount.ToString();

            return result;
        }

        private void UpdateTreeView(TreeNode branch, string info)
        {
            _treeView.BeginUpdate();

            branch.Nodes.Add(info);

            _treeView.EndUpdate();
        }
    }
}
