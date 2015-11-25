using EnvDTE;
using FilterSynchronizer.Model;
using Microsoft.VisualStudio.VCProjectEngine;
using System.Collections.Generic;
using System.Linq;

namespace FilterSynchronizer.Helpers
{
    static class SolutionHelper
    {
        public static IEnumerable<VCProjectItemWrapper> GetSelectedItems(FilterSynchronizerPackage package)
        {
            return GetSelectedUIHierarchyItems(package)
                .Where(item => item.Object is ProjectItem)
                .Select(item => item.Object as ProjectItem)
                .Where(item => item.Object is VCProjectItem)
                .Select(item => WrapperFactory.FromProjectItem(item.Object as VCProjectItem));
        }

        public static IEnumerable<VCProjectWrapper> GetSelectedProjects(FilterSynchronizerPackage package)
        {
            return GetSelectedItems(package)
                .Where(item => item is VCProjectWrapper)
                .Cast<VCProjectWrapper>();
        }

        public static VCProjectWrapper GetProjectOfSelection(FilterSynchronizerPackage package)
        {
            var containingProjects = new List<VCProjectWrapper>();
            containingProjects.AddRange(GetSelectedItems(package).Select(item => item.ContainingProject));

            if (containingProjects.Count() == 0)
                return null;

            if (containingProjects.Any(project => !project.Equals(containingProjects[0])))
                return null;

            return containingProjects[0];
        }

        private static UIHierarchy GetSolutionExplorer(FilterSynchronizerPackage package)
        {
            return package.IDE.ToolWindows.SolutionExplorer;
        }

        private static IEnumerable<UIHierarchyItem> GetSelectedUIHierarchyItems(FilterSynchronizerPackage package)
        {
            return ((object[])GetSolutionExplorer(package).SelectedItems)
                .Cast<UIHierarchyItem>();
        }
    }
}
