using Microsoft.VisualStudio.VCProjectEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Model
{
    abstract class VCItemWrapper
    {
        protected VCItemWrapper(VCProjectItem item)
        {
            Item = item;
        }

        public VCProjectItem Item { get; private set; }

        public string Name => Item.ItemName;

        public VCProjectWrapper ContainingProject => new VCProjectWrapper(Item.project);

        public VCItemWrapper Parent
        {
            get
            {
                if (Item.Parent == null)
                    return null;

                if (Item.Parent is VCProject)
                    return new VCProjectWrapper(Item.Parent);

                if (Item.Parent is VCFile)
                    return new VCFileWrapper(Item.Parent);

                throw new NotSupportedException();
            }
        }

        public string FilterPath
        {
            get
            {
                if (this is VCProjectWrapper)
                    return "";
                else
                    return Parent.FilterPath + "/" + Name;
            }
        }
    }
}
