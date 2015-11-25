using Microsoft.VisualStudio.VCProjectEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Model
{
    internal class VCFileWrapper : VCItemWrapper
    {
        public VCFileWrapper(VCFile file) : base(file)
        {
        }

        protected VCFile File => (VCFile)Item;

        public string FullPath => File.FullPath;

        public string RelativePath
        {
            get
            {
                string path = FullPath;
                string root = ContainingProject.ProjectRoot;

                Uri uri = new Uri(root);
                return uri.MakeRelativeUri(new Uri(path)).ToString();
            }
        }
    }
}
