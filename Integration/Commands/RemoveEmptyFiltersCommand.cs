using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCFileUtils.Helpers;
using VCFileUtils.Logic;
using VCFileUtils.Model;

namespace VCFileUtils.Integration.Commands
{
    class RemoveEmptyFiltersCommand : BaseCommand
    {
        public RemoveEmptyFiltersCommand(VCFileUtilsPackage package)
            : base(package, new CommandID(GuidList.GuidVCFileUtilsCommandSet, (int)PkgCmdIDList.CmdIDRemoveEmptyFilters))
        {

        }

        protected override void OnBeforeQueryStatus()
        {
            var selection = SolutionHelper.GetSelectedItems(Package).ToList();

            Visible = selection.Any() && selection.All(item => !(item is VCProjectWrapper));
            Enabled = selection.Any(item => item is ContainerWrapper);
        }

        protected override void OnExecute()
        {
            var selection = SolutionHelper.GetSelectedItems(Package)
                .Where(item => item is ContainerWrapper)
                .ToList();

            foreach (ContainerWrapper container in selection)
            {
                FileUtils.RemoveEmptyFilters(container);
            }
        }
    }
}
