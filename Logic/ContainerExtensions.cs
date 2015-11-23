using FilterSynchronizer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Logic
{
    static class ContainerExtensions
    {
        public static VCContainerWrapper CreateFilterPath(this VCContainerWrapper container, string path)
        {
            return CreateFilterPath(container, path.Split('/', '\\'));
        }

        public static VCContainerWrapper CreateFilterPath(this VCContainerWrapper container, string[] path)
        {
            if (path.Length == 0)
                return container;

            string nextName = path[0];

            if (String.IsNullOrEmpty(nextName))
                throw new ArgumentException("path");

            VCFilterWrapper nextFilter = container.GetFilter(nextName, true);
            string[] nextPath = path.Skip(1).ToArray();
            return CreateFilterPath(nextFilter, nextPath);
        }

        public static IEnumerable<VCFileWrapper> GetFilesRecursive(this VCContainerWrapper container)
        {
            var files = new List<VCFileWrapper>();
            GetFilesRecursive(container, files);
            return files;
        }

        private static void GetFilesRecursive(VCContainerWrapper container, List<VCFileWrapper> files)
        {
            files.AddRange(container.Files);

            foreach (VCContainerWrapper child in container.Filters)
            {
                GetFilesRecursive(child, files);
            }
        }
    }
}
