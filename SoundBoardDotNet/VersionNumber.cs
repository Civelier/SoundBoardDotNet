using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    [Serializable]
    public struct VersionNumber : ISerializable, IEquatable<VersionNumber>
    {
        public static VersionNumber Empty = new VersionNumber();

        public readonly int[] Number;
        public int Count { get { return Number.Length; } }

        public VersionNumber(params int[] version)
        {
            foreach (var number in version)
            {
                if (number < 0) throw new InvalidVersionException("A version number cannot be negative");
            }
            Number = version;
        }

        public VersionNumber(SerializationInfo info, StreamingContext context)
        {
            Number = (int[])info.GetValue("Version", typeof(int[]));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Version", Number);
        }

        public void UpdateVersion()
        {
            Number[Count - 1]++;
        }

        /// <summary>
        /// Increments the version by the specified index. Resets all folowing numbers
        /// </summary>
        /// <param name="indexFromLast">The index to increment starting by the last number</param>
        public void UpdateVersion(int indexFromLast)
        {
            Number[Count - indexFromLast - 1]++;
            for (int i = Count - indexFromLast; i < Count; i++)
            {
                Number[i] = 0;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Number.Length; i++)
            {
                if (i != 0) sb.Append(".");
                sb.Append(Number[i]);
            }
            return sb.ToString();
        }

        public bool Equals(VersionNumber other)
        {
            return this == other;
        }

        public static bool operator ==(VersionNumber v1, VersionNumber v2)
        {
            return v1.Number == v2.Number;
        }

        public static bool operator !=(VersionNumber v1, VersionNumber v2)
        {
            return !(v1 == v2);
        }
    }
}
