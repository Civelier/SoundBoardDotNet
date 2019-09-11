using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    public class GeneralVersion : IVersion
    {
        public GeneralVersion(VersionNumber number)
        {
            MyVersionNumber = number;
        }
        public override bool IsCompatible(VersionNumber version)
        {
            return IsFuture(version);
        }
    }
}
