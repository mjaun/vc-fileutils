using Microsoft.VisualStudio.VCProjectEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Model
{
    internal class VCFilterWrapper : VCContainerWrapper
    {
        public VCFilterWrapper(VCFilter filter) : base(filter)
        {
        }

        protected VCFilter Filter => (VCFilter)Item;

        protected override dynamic _Files => Filter.Files;
        protected override dynamic _Filters => Filter.Filters;
        
        protected override VCFilter _AddFilter(string name)
        {
            return (VCFilter)Filter.AddFilter(name);
        }

        protected override bool _CanAddFilter(string name)
        {
            return Filter.CanAddFilter(name);
        }
    }
}
