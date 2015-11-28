using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFileUtils.Helpers
{
    static class PathHelper
    {
        public static string GetRelativePath(string root, string path)
        {
            if (!root.EndsWith("\\") || !root.EndsWith("/"))
                root += Path.DirectorySeparatorChar;

            Uri uriRoot = new Uri(root);
            Uri uriPath = new Uri(path);
            return uriRoot.MakeRelativeUri(uriPath).ToString();
        }
    }
}
