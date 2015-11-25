using FilterSynchronizer.Model;
using System;
using System.IO;

namespace FilterSynchronizer.Logic
{
    static class FilterUtils
    {
        public static void SyncWithFileSystem(ContainerWrapper container)
        {
            foreach (VCFileWrapper file in container.GetFilesRecursive())
            {
                SyncWithFileSystem(file);
            }
        }

        public static void SyncWithFileSystem(VCFileWrapper file)
        {
            VCProjectWrapper project = file.ContainingProject;
            string filterPath = Path.GetDirectoryName(file.RelativePath);

            ContainerWrapper newParent;
            if (String.IsNullOrEmpty(filterPath))
                newParent = project;
            else
                newParent = project.CreateFilterPath(filterPath);

            if (!file.Parent.Equals(newParent))
                file.Move(newParent);
        }
    }
}
