//------------------------------------------------------------------------------
// <copyright file="VCFileUtilsPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using EnvDTE80;
using VCFileUtils.Integration;
using VCFileUtils.Integration.Commands;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace VCFileUtils
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.GuidVCFileUtilsPackageString)]
    public sealed class VCFileUtilsPackage : Package
    {
        #region Fields

        private DTE2 _ide;
        private OleMenuCommandService _menuCommandService;

        #endregion

        #region Constructors

        public VCFileUtilsPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #endregion

        #region Public Integration Properties

        public DTE2 IDE => _ide ?? (_ide = GetService(typeof(SApplicationObject)) as DTE2);

        public OleMenuCommandService MenuCommandService => _menuCommandService ?? (_menuCommandService = GetService(typeof(IMenuCommandService)) as OleMenuCommandService);

        #endregion

        #region Package Members

        protected override void Initialize()
        {
            base.Initialize();

            RegisterCommands();
        }

        #endregion

        #region Private Methods

        private void RegisterCommands()
        {
            var menuCommandService = MenuCommandService;

            if (menuCommandService != null)
            {
                menuCommandService.AddCommand(new SyncWithFileSystemCommand(this));
                menuCommandService.AddCommand(new SetProjectRootCommand(this));
            }
        }

        #endregion
    }
}
