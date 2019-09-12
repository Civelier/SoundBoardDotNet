using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    [Serializable]
    public abstract class IVersion
    {
        public VersionNumber MyVersionNumber;
        private Dictionary<VersionNumber, int> _supportedVersions = new Dictionary<VersionNumber, int>();
        private Dictionary<VersionNumber, Action<IVersion>> _updateActions = new Dictionary<VersionNumber, Action<IVersion>>();
        public int Count => MyVersionNumber.Count;
        public abstract bool IsCompatible(VersionNumber version);

        protected bool VerifyVersionFormat(VersionNumber version, bool throwException = false, string parameterName = "version")
        {
            if (version.Number == null)
            {
                if (throwException) throw new InvalidVersionException($"Version number was null");
                return false;
            }
            if (Count != version.Count)
            {
                if (throwException) throw new InvalidVersionException($"Version format was invalid! " +
                    $"Expected {parameterName} to have {Count} numbers but found {version.Count} numbers.");
                return false;
            }
            return true;
        }
        public bool IsAnterior(VersionNumber version)
        {
            VerifyVersionFormat(version, true);
            for (int i = 0; i < version.Count; i++)
            {
                if (MyVersionNumber.Number[i] > version.Number[i]) return true;
            }
            return false;
        }
        public bool IsFuture(VersionNumber version)
        {
            VerifyVersionFormat(version, true);
            for (int i = 0; i < version.Count; i++)
            {
                if (MyVersionNumber.Number[i] < version.Number[i]) return true;
            }
            return false;
        }

        public void AddSupportedVersion(VersionNumber version, int leadingIndex = 0)
        {
            if (leadingIndex == 0) leadingIndex = GetLeadingVersionNumberIndex(version);
            VerifyVersionFormat(version, true);
            if (version.Number[leadingIndex] == 0 && leadingIndex == 0) throw new InvalidVersionException("A supported version must at least have one number higher than 0");
            _supportedVersions.Add(version, leadingIndex);
        }

        private int GetLeadingVersionNumberIndex(VersionNumber version)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (version.Number[i] != 0) return i;
            }
            return 0;
        }
        
        public bool IsSupported(VersionNumber version)
        {
            if (version == VersionNumber.Empty)
            {
                if (_updateActions.TryGetValue(VersionNumber.Empty, out Action<IVersion> output)) return true;
            }
            if (!VerifyVersionFormat(version)) return false;

            foreach (var supportedVersion in _supportedVersions)
            {
                int index = supportedVersion.Value;
                bool isSupported = true;
                for (int i = 0; i <= index; i++)
                {
                    if (version.Number[i] != supportedVersion.Key.Number[i])
                    {
                        isSupported = false;
                        break;
                    }
                }
                if (isSupported) return true;
            }

            return false;
        }

        public bool Upgrade(IVersion version)
        {
            if (IsCompatible(version)) return true;
            if (!IsSupported(version)) return false;

            _updateActions[version](version);

            return true;
        }

        public static implicit operator VersionNumber(IVersion version)
        {
            if (version == null) return VersionNumber.Empty;
            return version.MyVersionNumber;
        }
    }
}
