using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VCFileUtils.Helpers;
using VCFileUtils.Logic;
using VCFileUtils.Model;
using VCFileUtils.UI;

namespace VCFileUtils.Integration.Commands
{
    class AddFilesOrganizedCommand : BaseCommand
    {
        public AddFilesOrganizedCommand(VCFileUtilsPackage package)
            : base(package, new CommandID(GuidList.GuidVCFileUtilsCommandSet, (int)PkgCmdIDList.CmdIDAddFilesOrganized))
        {
        }

        protected override void OnBeforeQueryStatus()
        {
            Visible = SolutionHelper.GetSelectedItems(Package).Any();
            Enabled = SolutionHelper.GetSelectedDirectory(Package) != null;
        }

        protected override void OnExecute()
        {
            var dlg = new AddFilesDialog();
            dlg.RootPath = SolutionHelper.GetSelectedDirectory(Package);
            var project = SolutionHelper.GetProjectOfSelection(Package);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in dlg.SelectedFiles)
                {
                    try
                    {
                        FileUtils.AddFileOrganized(project, path);
                    }
                    catch
                    {
                        MessageBox.Show("Error: Could not add file " + path + "!", "VC File Utils", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
