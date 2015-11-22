using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterSynchronizer.Integration
{
    public static class GuidList
    {
        public const string GuidFilterSynchronizerPackageString = "AD0E4393-9BAE-4B79-81A3-9E648613264C";
        public static readonly Guid GuidFilterSynchronizerPackage = new Guid(GuidFilterSynchronizerPackageString);

        public static readonly Guid GuidFilterSynchronizerCommandSet = new Guid("E35CFF27-48F1-4606-99A6-678C89456042");
    }
}
