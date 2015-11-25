using Microsoft.VisualStudio.VCProjectEngine;
using System;

namespace FilterSynchronizer.Model
{
    abstract class VCProjectItemWrapper
    {
        protected VCProjectItemWrapper(VCProjectItem item)
        {
            VCProjectItem = item;
        }

        public VCProjectItem VCProjectItem { get; private set; }

        public string Name => VCProjectItem.ItemName;

        public VCProjectWrapper ContainingProject => new VCProjectWrapper(VCProjectItem.project);

        public VCProjectItemWrapper Parent
        {
            get
            {
                if (VCProjectItem.Parent == null)
                    return null;
                else
                    return WrapperFactory.FromProjectItem(VCProjectItem.Parent);
            }
        }

        public string FilterPath
        {
            get
            {
                if (this is VCProjectWrapper)
                    return "";
                else
                    return Parent.FilterPath + "/" + Name;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is VCProjectItemWrapper))
                return false;

            return VCProjectItem.Equals((obj as VCProjectItemWrapper).VCProjectItem);
        }

        public override int GetHashCode()
        {
            return VCProjectItem.GetHashCode();
        }

        public override string ToString()
        {
            return FilterPath;
        }
    }
}
