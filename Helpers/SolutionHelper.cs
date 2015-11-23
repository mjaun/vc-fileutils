using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Helpers
{
    static class SolutionHelper
    {
        public static IEnumerable<ProjectItem> GetSelectedProjectItems(FilterSynchronizerPackage package)
        {
            return UIHierarchyHelper.GetSelectedUIHierarchyItems(package)
                .Select(item => item.Object)
                .Where(item => item is ProjectItem)
                .Cast<ProjectItem>();
        }

        public static IEnumerable<Project> GetSelectedProjects(FilterSynchronizerPackage package)
        {
            return UIHierarchyHelper.GetSelectedUIHierarchyItems(package)
                .Select(item => item.Object)
                .Where(item => item is Project)
                .Cast<Project>();
        }

        public static Project GetProjectOfSelection(FilterSynchronizerPackage package)
        {
            var containingProjects = new List<Project>();
            containingProjects.AddRange(GetSelectedProjects(package));
            containingProjects.AddRange(GetSelectedProjectItems(package).Select(item => item.ContainingProject));

            if (containingProjects.Count() == 0)
                return null;

            if (containingProjects.Any(project => project != containingProjects[0]))
                return null;

            return containingProjects[0];
        }
    }
}
