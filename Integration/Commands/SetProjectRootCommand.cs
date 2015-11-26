using VCFileUtils.Helpers;
using VCFileUtils.Model;
using System.ComponentModel.Design;
using System.IO;
using System.Windows.Forms;

namespace VCFileUtils.Integration.Commands
{
    class SetProjectRootCommand : BaseCommand
    {
        public SetProjectRootCommand(VCFileUtilsPackage package)
            : base(package, new CommandID(GuidList.GuidVCFileUtilsCommandSet, (int)PkgCmdIDList.CmdIDSetProjectRoot))
        {

        }

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
                ExtensionSettings settings = SettingsManager.GetSettings(project);
                settings.ProjectRoot = dlg.SelectedPath;
                SettingsManager.SaveSettings(project);
            }
        }
    }
}
