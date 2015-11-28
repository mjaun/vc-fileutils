using Microsoft.VisualStudio.VCProjectEngine;
using System;
using VCFileUtils.Helpers;

namespace VCFileUtils.Model
{
    internal class VCFileWrapper : VCProjectItemWrapper, IMovable
    {
        public VCFileWrapper(VCFile file) : base(file)
        {
        }


        protected VCFile VCFile 
        {
            get
            {
                return (VCFile)VCProjectItem;
            }
        }

        public override string FilePath
        {
            get
            {
                return VCFile.FullPath;
            }
        }

        public string RelativePath
        {
            get
            {
                return ContainingProject.GetRelativePathOf(VCFile.FullPath);
            }
        }

        public void Move(ContainerWrapper newParent)
        {
            if (!VCFile.CanMove(newParent.VCProjectItem))
                throw new InvalidOperationException();

            VCFile.Move(newParent.VCProjectItem);
        }

        public void Remove()
        {
            VCFile.Remove();
        }
    }
}
