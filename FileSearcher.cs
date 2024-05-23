using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSearch
{
    internal class FileSearcher
    {
        private delegate void FileSearcherAction(TreeNode node);

        private readonly TreeView _treeView;
        private Regex _regex;
        private TreeNode _root;
        private LinkedList<TreeNode> _activeBranch;

        public FileSearcher(TreeView treeView) 
        {
            _treeView = treeView;
        }

        public async void Search(string path, string pattern)
        {
            _root = new TreeNode(path);
            _regex = new Regex(pattern);
            _activeBranch = new LinkedList<TreeNode>();

            _activeBranch.AddFirst(_root);
            _treeView.Nodes.Add(_root);

            await Task.Run(() => BuildBranchNodes(_root));
        }

        private void BuildBranchNodes(TreeNode branch)
        {
            BuildFileNodes(branch);

            try
            {
                foreach (var dir in Directory.GetDirectories(branch.Text))
                {
                    var newBranch = new TreeNode(dir);

                    UpdateFileSearcher(newBranch, BuildBranchNodes);
                }
            }
            catch (Exception)
            {
                throw;
            }           
        }

        private void LinkTreeNodes(TreeNode node)
        {
            var last = _activeBranch.FindLast(node);

            while (last.Value.Parent == null && last.Value != _root)
            {
                UpdateTreeNode(last.Value, last.Previous.Value);

                last = last.Previous;
            }
        }

        private void UpdateFileSearcher(TreeNode node, FileSearcherAction action)
        {
            _activeBranch.AddLast(node);  
            
            action.Invoke(node);

            _activeBranch.RemoveLast();
        }

        private void BuildFileNodes(TreeNode parent)
        {
            try
            {
                foreach (var file in Directory.GetFiles(parent.Text))
                {
                    var fileName = Path.GetFileName(file);

                    if (_regex.IsMatch(fileName))
                    {
                        var fileNode = new TreeNode(file);

                        UpdateFileSearcher(fileNode, LinkTreeNodes);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateTreeNode(TreeNode node, TreeNode branch)
        {
            //node.Text = Path.GetFileName(node.Text);

            _treeView.BeginInvoke(new Action(() =>
            {
                _treeView.BeginUpdate();

                if (!branch.Nodes.Contains(node))
                {
                    branch.Nodes.Add(node);
                }

                _treeView.EndUpdate();
            }));
        }
    }
}
