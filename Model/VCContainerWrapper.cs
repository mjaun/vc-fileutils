using Microsoft.VisualStudio.VCProjectEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Model
{
    abstract class VCContainerWrapper : VCItemWrapper
    {
        protected VCContainerWrapper(VCProjectItem obj) : base(obj)
        {
        }

        protected abstract dynamic _Files { get; }
        protected abstract dynamic _Filters { get; }

        protected abstract VCFilter _AddFilter(string name);
        protected abstract bool _CanAddFilter(string name);

        public IEnumerable<VCFileWrapper> Files
        {
            get
            {
                return ((IVCCollection)_Files)
                    .Cast<VCFile>()
                    .Select(file => new VCFileWrapper(file));
            }
        }

        public IEnumerable<VCFilterWrapper> Filters
        {
            get
            {
                return ((IVCCollection)_Filters)
                    .Cast<VCFilter>()
                    .Select(filter => new VCFilterWrapper(filter));
            }
        }

        public VCFilterWrapper AddFilter(string name)
        {
            if (!_CanAddFilter(name))
            {
                throw new ArgumentException();
            }

            return new VCFilterWrapper(_AddFilter(name));
        }

        public VCFilterWrapper GetFilter(string name, bool create = false)
        {
            VCFilterWrapper filter = Filters
                .FirstOrDefault(f => f.Name == name);

            if (filter != null)
                return filter;

            if (create)
                return AddFilter(name);

            throw new KeyNotFoundException();
        }
    }
}
