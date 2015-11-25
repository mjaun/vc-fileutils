using Microsoft.VisualStudio.VCProjectEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Model
{
    internal class VCProjectWrapper : VCContainerWrapper
    {
        public VCProjectWrapper(VCProject project) : base(project)
        {
        }

        protected VCProject Project => (VCProject)Item;

        protected override dynamic _Files => Project.Files;
        protected override dynamic _Filters => Project.Filters;

        public string ProjectFile => Project.ProjectFile;

        protected override VCFilter _AddFilter(string name)
        {
            return (VCFilter)Project.AddFilter(name);
        }

        protected override bool _CanAddFilter(string name)
        {
            return Project.CanAddFilter(name);
        }

        public string ProjectRoot
        {
            get
            {
                return Path.GetDirectoryName(ProjectFile);
            }
        }
    }
}
