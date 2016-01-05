using VCFileUtils.Model;
using System;
using System.IO;
using System.Linq;
using VCFileUtils.Helpers;
using System.Windows.Forms;

namespace VCFileUtils.Logic
{
    static class FileUtils
    {
        public static void OrganizeFileInProject(VCFileWrapper file)
        {
            if (file.RelativePath == null)
                throw new InvalidOperationException("project root not set");

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

        public static VCFileWrapper ReAddFile(VCFileWrapper file)
        {
            ContainerWrapper parent = file.Parent;
            string path = file.FullPath;
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

        public static void OrganizeFileOnDisk(VCFileWrapper file)
        {
            string root = file.ContainingProject.GetProjectRoot();

            if (root == null)
                throw new InvalidOperationException("project root not set");

            string filePath = PathHelper.GetAbsolutePath(root, file.FilterPath);

            if (filePath == file.FullPath)
                return;

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.Move(file.FullPath, filePath);

                ContainerWrapper parent = file.Parent;
                file.Remove();
                parent.AddFile(filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not move file: " + e.Message, "VC File Utilities", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
