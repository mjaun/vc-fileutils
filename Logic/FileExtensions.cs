using FilterSynchronizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Logic
{
    static class FileExtensions
    {
        public static string GetRelativePath(this VCFileWrapper file)
        {
            string path = file.FullPath;
            string root = file.ContainingProject.GetProjectRoot();

            Uri uri = new Uri(root);
            return uri.MakeRelativeUri(new Uri(path)).ToString();
        }
    }
}
