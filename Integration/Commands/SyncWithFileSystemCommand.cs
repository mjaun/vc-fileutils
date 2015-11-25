using FilterSynchronizer.Helpers;
using FilterSynchronizer.Logic;
using FilterSynchronizer.Model;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace FilterSynchronizer.Integration.Commands
{
    class SyncWithFileSystemCommand : BaseCommand
    {
        #region Constructors

        public SyncWithFileSystemCommand(FilterSynchronizerPackage package)
            : base(package, new CommandID(GuidList.GuidFilterSynchronizerCommandSet, (int)PkgCmdIDList.CmdIDSyncWithFileSystem))
        {
        }

        #endregion

        #region BaseCommand Members

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
                    FilterUtils.SyncWithFileSystem(item as VCFileWrapper);

                if (item is ContainerWrapper)
                    FilterUtils.SyncWithFileSystem(item as ContainerWrapper);
            }
        }

        #endregion BaseCommand Members
    }
}
