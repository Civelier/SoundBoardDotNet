using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundBoardDotNet.PlayHeads
{
    public interface IPlayHead : INotifyPropertyChanged
    {
        double Seconds { get; set; }
        double Progression { get; set; }
        double TotalSeconds { get; }
        int ParentWidth { get; }
        int WaveViewerWidth { set; }
        Panel CursorPanel { get; }
        Panel ParentPanel { set; }
        int ScrollX { get; set; }
        int PointingX { get; set; }
        int XLocation { get; set; }
        int ParentOffset { set; }
        SoundWaveViewer Viewer { set; }

        void UpdateLocationFromSeconds();
    }
}
