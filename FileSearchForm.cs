using System.ComponentModel;
using System.Windows.Forms;

namespace FileSearch
{
    public partial class FileSearchForm : Form
    {
        public FileSearchForm()
        {
            InitializeComponent();
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

            var fileSearcher = new FileSearcher(FileExplorer);
            var path = FolderBrowserDialog.SelectedPath;
            var pattern = FileRegEx.Text.Trim();

            fileSearcher.Search(path, pattern);
        }

        private void CancelSearchBtn_Click(object sender, System.EventArgs e)
        {

        }
    }
}
