using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCFileUtils.UI
{
    class FileSystemTreeView : TreeView
    {
        public FileSystemTreeView()
        {
            CheckBoxes = true;
            ImageList = new ImageList();
        }

        private string _rootPath;
        public string RootPath
        {
            get
            {
                return _rootPath;
            }

            set
            {
                _rootPath = value;
                RefreshContent();
            }
        }

        public void RefreshContent()
        {
            BeginUpdate();
            Nodes.Clear();

            if (!String.IsNullOrEmpty(RootPath) && Directory.Exists(RootPath))
            {
                TreeNode rootNode = CreateDirectoryNode(new DirectoryInfo(RootPath));
                Nodes.Add(rootNode);
                rootNode.ExpandAll();
            }

            EndUpdate();
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);

            foreach (var directory in directoryInfo.GetDirectories())
            { 
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                var node = new TreeNode(file.Name);

                if (!ImageList.Images.ContainsKey(file.Extension))
                {
                    var icon = Icon.ExtractAssociatedIcon(file.FullName);
                    ImageList.Images.Add(file.Extension, icon);
                }

                node.ImageKey = file.Extension;
                directoryNode.Nodes.Add(node);
            }

            return directoryNode;
        }
    }
}
