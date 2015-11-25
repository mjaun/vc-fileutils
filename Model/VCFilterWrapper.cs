using Microsoft.VisualStudio.VCProjectEngine;
using System;

namespace FilterSynchronizer.Model
{
    internal class VCFilterWrapper : ContainerWrapper, IMovable
    {
        public VCFilterWrapper(VCFilter filter) : base(filter)
        {
        }

        protected VCFilter Filter => (VCFilter)VCProjectItem;

        protected override dynamic _Files => Filter.Files;
        protected override dynamic _Filters => Filter.Filters;
        
        protected override VCFilter _AddFilter(string name)
        {
            if (!Filter.CanAddFilter(name))
                throw new InvalidOperationException();

            return (VCFilter)Filter.AddFilter(name);
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
