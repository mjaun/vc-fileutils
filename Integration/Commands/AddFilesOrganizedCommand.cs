using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCFileUtils.Helpers;

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
            Visible = false;
            //Visible = SolutionHelper.GetSelectedItems(Package).Any();
            //Enabled = SolutionHelper.GetDirectoryOfSelection(Package) != null;
        }

        protected override void OnExecute()
        {

        }
    }
}
