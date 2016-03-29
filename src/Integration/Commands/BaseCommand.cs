using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;

namespace VCFileUtils.Integration.Commands
{
    abstract class BaseCommand : OleMenuCommand
    {
        #region Constructors

        protected BaseCommand(VCFileUtilsPackage package, CommandID id)
            : base(BaseCommand_Execute, id)
        {
            Package = package;

            BeforeQueryStatus += BaseCommand_BeforeQueryStatus;
        }

        #endregion Constructors

        #region Properties

        protected VCFileUtilsPackage Package { get; private set; }

        #endregion Properties

        #region Event Handlers

        private static void BaseCommand_BeforeQueryStatus(object sender, EventArgs e)
        {
            BaseCommand command = sender as BaseCommand;
            if (command != null)
            {
                command.OnBeforeQueryStatus();
            }
        }

        private static void BaseCommand_Execute(object sender, EventArgs e)
        {
            BaseCommand command = sender as BaseCommand;
            if (command != null)
            {
                command.OnExecute();
            }
        }

        #endregion Event Handlers

        #region Methods

        protected virtual void OnBeforeQueryStatus()
        {

        }

        protected virtual void OnExecute()
        {

        }

        #endregion Methods
    }
}