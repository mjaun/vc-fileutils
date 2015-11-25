using FilterSynchronizer.Helpers;
using FilterSynchronizer.Model;
using System.ComponentModel.Design;
using System.IO;
using System.Windows.Forms;

namespace FilterSynchronizer.Integration.Commands
{
    class SetProjectRootCommand : BaseCommand
    {
        #region Constructors

        public SetProjectRootCommand(FilterSynchronizerPackage package)
            : base(package, new CommandID(GuidList.GuidFilterSynchronizerCommandSet, (int)PkgCmdIDList.CmdIDSetProjectRoot))
        {

        }

        #endregion

        #region BaseCommand Members

        protected override void OnBeforeQueryStatus()
        {
            Visible = SolutionHelper.GetProjectOfSelection(Package) != null;
            Enabled = true;
        }

        protected override void OnExecute()
        {
            VCProjectWrapper project = SolutionHelper.GetProjectOfSelection(Package);

            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = Path.GetDirectoryName(project.ProjectFile);
            dlg.ShowNewFolderButton = false;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

        #endregion BaseCommand Members
    }
}
