using VCFileUtils.Helpers;
using VCFileUtils.Logic;
using VCFileUtils.Model;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System;
using System.IO;

namespace VCFileUtils.Integration.Commands
{
    class OrganizeOnDiskCommand : BaseCommand
    {
        public OrganizeOnDiskCommand(VCFileUtilsPackage package)
            : base(package, new CommandID(GuidList.GuidVCFileUtilsCommandSet, (int)PkgCmdIDList.CmdIDOrganizeOnDisk))
        {
        }

        protected override void OnBeforeQueryStatus()
        {
            var selection = SolutionHelper.GetSelectedItems(Package).ToList();

            Visible = selection.Any() && selection.All(item => !(item is VCProjectWrapper));
            Enabled = selection.All(item => item.ContainingProject.GetProjectRoot() != null);
        }

        protected override void OnExecute()
        {
            var selectedFiles = SolutionHelper.GetSelectedFiles(Package).ToList();

            foreach (var file in selectedFiles)
            {
                FileUtils.OrganizeFileOnDisk(file);
            }
        }
    }
}
