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
        public static string GetRelativePath(string root, string pathAbs)
        {
            if (!root.EndsWith("\\") && !root.EndsWith("/"))
                root += Path.DirectorySeparatorChar;

            Uri uriRoot = new Uri(root);
            Uri uriPath = new Uri(pathAbs);
            Uri uriRelative = uriRoot.MakeRelativeUri(uriPath);
            return Uri.UnescapeDataString(uriRelative.ToString());
        }

        public static string GetAbsolutePath(string root, string pathRel)
        {
            if (!root.EndsWith("\\") && !root.EndsWith("/"))
                root += Path.DirectorySeparatorChar;

            UriBuilder builder = new UriBuilder(root);
            builder.Path += pathRel;
            string absolutePath = Uri.UnescapeDataString(builder.Uri.AbsolutePath);
            return absolutePath.Replace('/', Path.DirectorySeparatorChar);
        }
    }
}
