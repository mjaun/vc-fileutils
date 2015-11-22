using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Helpers
{
    /// <summary>
    /// A static helper class for working with the UI hierarchies.
    /// </summary>
    internal static class UIHierarchyHelper
    {
        /// <summary>
        /// Gets the solution explorer for the specified hosting package.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        /// <returns>The solution explorer.</returns>
        internal static UIHierarchy GetSolutionExplorer(FilterSynchronizerPackage package)
        {
            return package.IDE.ToolWindows.SolutionExplorer;
        }

        /// <summary>
        /// Gets an enumerable set of the selected UI hierarchy items.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        /// <returns>The enumerable set of selected UI hierarchy items.</returns>
        internal static IEnumerable<UIHierarchyItem> GetSelectedUIHierarchyItems(FilterSynchronizerPackage package)
        {
            return ((object[])GetSolutionExplorer(package).SelectedItems)
                .Cast<UIHierarchyItem>();
        }
    }
}
