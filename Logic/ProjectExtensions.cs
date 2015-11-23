using FilterSynchronizer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Logic
{
    static class ProjectExtensions
    {
        public static string GetProjectRoot(this VCProjectWrapper project)
        {
            return Path.GetDirectoryName(project.ProjectFile);
        }
    }
}
