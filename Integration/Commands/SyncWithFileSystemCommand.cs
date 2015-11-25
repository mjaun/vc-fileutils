using EnvDTE;
using FilterSynchronizer.Helpers;
using Microsoft.VisualStudio.VCProjectEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Visible = SolutionHelper.GetProjectOfSelection(Package).Object is VCProject;
        }

        protected override void OnExecute()
        {
            VCProject project = SolutionHelper.GetProjectOfSelection(Package).Object as VCProject;
        }

        #endregion BaseCommand Members
    }
}
