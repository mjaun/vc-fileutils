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

        public string Name 
        {
            get
            {
                return VCProjectItem.ItemName;
            }
        }

        public VCProjectWrapper ContainingProject
        {
            get
            {
                return new VCProjectWrapper(VCProjectItem.project);
            }
        }

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

        public abstract string FullPath { get; }

        public string FilterPath
        {
            get
            {
                if (this is VCProjectWrapper)
                    return "";

                string parentPath = Parent.FilterPath;

                if (!String.IsNullOrEmpty(parentPath))
                    return parentPath + "/" + Name;
                else
                    return Name;
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
