using Microsoft.VisualStudio.VCProjectEngine;
using System;

namespace VCFileUtils.Model
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

        public ContainerWrapper Parent
        {
            get
            {
                if (VCProjectItem.Parent == null)
                    return null;
                else
                    return (ContainerWrapper)WrapperFactory.FromVCProjectItem(VCProjectItem.Parent);
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
