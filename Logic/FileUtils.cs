using VCFileUtils.Model;
using System;
using System.IO;

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
            string filterPath = project.GetRelativePath(path);
            VCFilterWrapper parent = project.CreateFilterPath(Path.GetDirectoryName(filterPath));
            return parent.AddFile(path);
        }

        public static VCFileWrapper ReAddFile(VCFileWrapper file)
        {
            ContainerWrapper parent = file.Parent;
            string path = file.FullPath;
            file.Remove();
            return parent.AddFile(path);
        }
    }
}
