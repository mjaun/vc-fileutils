using FilterSynchronizer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Logic
{
    static class ItemExtensions
    {
        public static string GetFilterPath(this VCItemWrapper item)
        {
            string ret = item.Name;
            VCItemWrapper curr = item;
            
            while (!(curr is VCProjectWrapper))
            {
                ret = curr.Name + '/' + ret;
            }

            return ret;
        }
    }
}
