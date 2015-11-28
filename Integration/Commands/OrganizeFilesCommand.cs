using VCFileUtils.Helpers;
using VCFileUtils.Logic;
using VCFileUtils.Model;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System;

namespace VCFileUtils.Integration.Commands
{
    class OrganizeFilesCommand : BaseCommand
    {
        public OrganizeFilesCommand(VCFileUtilsPackage package)
            : base(package, new CommandID(GuidList.GuidVCFileUtilsCommandSet, (int)PkgCmdIDList.CmdIDOrganizeFiles))
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
            var selection = new List<VCProjectItemWrapper>(SolutionHelper.GetSelectedItems(Package));

            foreach (VCProjectItemWrapper item in selection)
            {
                if (item is VCFileWrapper)
                {
                    FileUtils.OrganizeFile(item as VCFileWrapper);
                }

                if (item is ContainerWrapper)
                {
                    foreach (VCFileWrapper file in (item as ContainerWrapper).GetFilesRecursive())
                    {
                        FileUtils.OrganizeFile(file);
                    }
                }
            }
        }
    }
}
