using VCFileUtils.Model;
using System;
using System.IO;
using System.Linq;

namespace VCFileUtils.Logic
{
    static class FileUtils
    {
        public static void OrganizeFile(VCFileWrapper file)
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

        public static VCFileWrapper AddFileOrganized(VCProjectWrapper project, string path)
        {
            string filterPath = project.MakeRelativePath(path);
            VCFilterWrapper parent = project.CreateFilterPath(Path.GetDirectoryName(filterPath));
            return parent.AddFile(path);
        }

        public static VCFileWrapper ReAddFile(VCFileWrapper file)
        {
            ContainerWrapper parent = file.Parent;
            string path = file.FilePath;
            file.Remove();
            return parent.AddFile(path);
        }

        public static void RemoveEmptyFilters(ContainerWrapper container)
        {
            foreach (VCFilterWrapper child in container.Filters)
                RemoveEmptyFilters(child);

            if (!container.Filters.Any() && !container.Files.Any() && container is VCFilterWrapper)
                (container as VCFilterWrapper).Remove();
        }
    }
}
