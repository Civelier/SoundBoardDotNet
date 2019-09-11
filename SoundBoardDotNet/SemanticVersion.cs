using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    [Serializable]
    public class SemanticVersion : IVersion
    {
        public int Major => MyVersionNumber.Number[0];
        public int Minor => MyVersionNumber.Number[1];
        public int Patch => MyVersionNumber.Number[2];
        public SemanticVersion()
        {
            MyVersionNumber = new VersionNumber(0, 0, 0);
        }
        public SemanticVersion(int major, int minor, int patch)
        {
            MyVersionNumber = new VersionNumber(major, minor, patch);
        }

        public override bool IsCompatible(VersionNumber version)
        {
            if (version.Number[0] == Major) return true;
            return false;
        }
    }
}
