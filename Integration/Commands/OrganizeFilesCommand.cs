using VCFileUtils.Helpers;
using VCFileUtils.Logic;
using VCFileUtils.Model;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

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
            Visible = SolutionHelper.GetSelectedItems(Package).Any();
            Enabled = true;
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
