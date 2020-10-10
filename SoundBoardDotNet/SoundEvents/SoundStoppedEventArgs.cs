using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet.SoundEvents
{
    public delegate void SoundStoppedEventHandler(AudioSound sender, SoundStoppedEventArgs args);
    public class SoundStoppedEventArgs
    {
        public enum SoundStopReason
        {
            ReachedEnd,
            Disposed,
            StoppedManually
        }

        public SoundStopReason Reason { get; private set; }

        internal static SoundStoppedEventArgs Disposed()
        {
            return new SoundStoppedEventArgs() { Reason = SoundStopReason.Disposed };
        }

        internal static SoundStoppedEventArgs StoppedManually()
        {
            return new SoundStoppedEventArgs() { Reason = SoundStopReason.StoppedManually };
        }

        internal static SoundStoppedEventArgs ReachedEnd()
        {
            return new SoundStoppedEventArgs() { Reason = SoundStopReason.ReachedEnd };
        }
    }
}
