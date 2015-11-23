using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Helpers
{
    static class UIHierarchyHelper
    {
        public static UIHierarchy GetSolutionExplorer(FilterSynchronizerPackage package)
        {
            return package.IDE.ToolWindows.SolutionExplorer;
        }

        public static IEnumerable<UIHierarchyItem> GetSelectedUIHierarchyItems(FilterSynchronizerPackage package)
        {
            return ((object[])GetSolutionExplorer(package).SelectedItems)
                .Cast<UIHierarchyItem>();
        }
    }
}
