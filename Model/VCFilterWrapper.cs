using Microsoft.VisualStudio.VCProjectEngine;
using System;
using System.IO;
using VCFileUtils.Helpers;

namespace VCFileUtils.Model
{
    internal class VCFilterWrapper : ContainerWrapper, IMovable
    {
        public VCFilterWrapper(VCFilter filter) : base(filter)
        {
        }

        protected VCFilter Filter => (VCFilter)VCProjectItem;

        protected override dynamic _Files => Filter.Files;
        protected override dynamic _Filters => Filter.Filters;

        public override string FilePath => Path.Combine(SettingsManager.GetSettings(ContainingProject).ProjectRoot, FilterPath);

        protected override VCFilter _AddFilter(string name)
        {
            if (!Filter.CanAddFilter(name))
                throw new InvalidOperationException();

            return (VCFilter)Filter.AddFilter(name);
        }

        protected override VCFile _AddFile(string path)
        {
            if (!Filter.CanAddFile(path))
                throw new InvalidOperationException();

            return (VCFile)Filter.AddFile(path);
        }

        public void Move(ContainerWrapper newParent)
        {
            if (!Filter.CanMove(newParent))
                throw new InvalidOperationException();

            Filter.Move(newParent.VCProjectItem);
        }

        public void Remove()
        {
            Filter.Remove();
        }
    }
}
