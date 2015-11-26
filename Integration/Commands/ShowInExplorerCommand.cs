using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCFileUtils.Helpers;
using VCFileUtils.Model;

namespace VCFileUtils.Integration.Commands
{
    class ShowInExplorerCommand : BaseCommand
    {
        public ShowInExplorerCommand(VCFileUtilsPackage package)
            : base(package, new CommandID(GuidList.GuidVCFileUtilsCommandSet, (int)PkgCmdIDList.CmdIDShowInExplorer))
        {

        }

        protected override void OnBeforeQueryStatus()
        {
            Visible = SolutionHelper.GetSelectedItems(Package).Any();
            Enabled = SolutionHelper.GetDirectoryOfSelection(Package) != null;
        }

        protected override void OnExecute()
        {
            Process.Start("explorer.exe", SolutionHelper.GetDirectoryOfSelection(Package));
        }
    }
}
