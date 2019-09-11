using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    [Serializable]
    public class FileVersion : IVersion
    {
        public int Major => MyVersionNumber.Number[0];
        public int Patch => MyVersionNumber.Number[1];

        public FileVersion(int major, int patch)
        {
            MyVersionNumber = new VersionNumber(major, patch);
        }
        public override bool IsCompatible(VersionNumber version)
        {
            if (!VerifyVersionFormat(version)) return false;
            return Major == version.Number[0];
        }
    }
}
