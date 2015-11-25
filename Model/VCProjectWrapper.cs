using Microsoft.VisualStudio.VCProjectEngine;
using System.IO;
using System;

namespace VCFileUtils.Model
{
    internal class VCProjectWrapper : ContainerWrapper
    {
        public VCProjectWrapper(VCProject project) : base(project)
        {
        }

        protected VCProject Project => (VCProject)VCProjectItem;

        protected override dynamic _Files => Project.Files;
        protected override dynamic _Filters => Project.Filters;

        public string ProjectFile => Project.ProjectFile;

        protected override VCFilter _AddFilter(string name)
        {
            if (!Project.CanAddFilter(name))
                throw new InvalidOperationException();

            return (VCFilter)Project.AddFilter(name);
        }
    }
}
