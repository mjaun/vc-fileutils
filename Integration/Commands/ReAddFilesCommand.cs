using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using VCFileUtils.Helpers;
using VCFileUtils.Model;

namespace VCFileUtils.Integration.Commands
{
    class ReAddFilesCommand : BaseCommand
    {
        public ReAddFilesCommand(VCFileUtilsPackage package)
            : base(package, new CommandID(GuidList.GuidVCFileUtilsCommandSet, (int)PkgCmdIDList.CmdIDReAddFiles))
        {
        }

        protected override void OnBeforeQueryStatus()
        {
            var selection = SolutionHelper.GetSelectedItems(Package).ToList();

            Visible = selection.Any() && selection.All(item => !(item is VCProjectWrapper));
            Enabled = SolutionHelper.GetSelectedFiles(Package).Any();
        }

        protected override void OnExecute()
        {
            var selectedFiles = SolutionHelper.GetSelectedFiles(Package).ToList();

            foreach (var file in selectedFiles)
            {
                var path = file.FullPath;
                var parent = file.Parent;

                file.Remove();
                parent.AddFile(path);
            }
        }
    }
}
