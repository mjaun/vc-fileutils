using Microsoft.VisualStudio.VCProjectEngine;
using System.IO;
using System;
using VCFileUtils.Helpers;

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

        protected override VCFilter _AddFilter(string name)
        {
            if (!Project.CanAddFilter(name))
                throw new InvalidOperationException();

            return (VCFilter)Project.AddFilter(name);
        }

        protected override VCFile _AddFile(string path)
        {
            if (!Project.CanAddFile(path))
                throw new InvalidOperationException();

            return (VCFile)Project.AddFile(path);
        }

        public string ProjectFile => Project.ProjectFile;

        public string GetRelativePath(string path)
        {
            string root = SettingsManager.GetSettings(this).ProjectRoot + "\\";

            Uri uriRoot = new Uri(root);
            Uri uriPath = new Uri(path);
            return uriRoot.MakeRelativeUri(uriPath).ToString();
        }
    }
}
