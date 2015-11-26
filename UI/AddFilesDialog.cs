using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCFileUtils.UI
{
    public partial class AddFilesDialog : Form
    {
        public AddFilesDialog()
        {
            InitializeComponent();
        }

        public string RootPath
        {
            get
            {
                return treeView.RootPath;
            }
            set
            {
                treeView.RootPath = value;
            }
        }

        public string[] SelectedFiles
        {
            get
            {
                return new string[0];
            }
        }
    }
}
