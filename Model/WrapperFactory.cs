using Microsoft.VisualStudio.VCProjectEngine;
using System;

namespace VCFileUtils.Model
{
    static class WrapperFactory
    {
        public static VCProjectItemWrapper FromVCProjectItem(VCProjectItem item)
        {
            if (item is VCFile)
                return new VCFileWrapper(item as VCFile);

            if (item is VCFilter)
                return new VCFilterWrapper(item as VCFilter);

            if (item is VCProject)
                return new VCProjectWrapper(item as VCProject);

            throw new NotSupportedException("Unknown ProjectItem type");
        }
    }
}
