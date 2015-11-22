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
    /// <summary>
    /// A static helper class for working with the solution.
    /// </summary>
    internal static class SolutionHelper
    {
        /// <summary>
        /// Returns the selected project items.
        /// </summary>
        /// <param name="package"></param>
        /// <returns>The selected project items.</returns>
        internal static IEnumerable<ProjectItem> GetSelectedProjectItems(FilterSynchronizerPackage package)
        {
            return UIHierarchyHelper.GetSelectedUIHierarchyItems(package)
                .Select(item => item.Object)
                .Where(item => item is ProjectItem)
                .Cast<ProjectItem>();
        }

        /// <summary>
        /// Returns the selected projects.
        /// </summary>
        /// <param name="package"></param>
        /// <returns>The selected projects.</returns>
        internal static IEnumerable<Project> GetSelectedProjects(FilterSynchronizerPackage package)
        {
            return UIHierarchyHelper.GetSelectedUIHierarchyItems(package)
                .Select(item => item.Object)
                .Where(item => item is Project)
                .Cast<Project>();
        }

        /// <summary>
        /// Returns the project of the selected project items. Returns null, if there
        /// are no items selected or if the selected items belong to different projects.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        /// <returns>The project of the selected project items.</returns>
        internal static Project GetProjectOfSelection(FilterSynchronizerPackage package)
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
