using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    public class SelectSoundFactory
    {
        private static SelectSoundFactory _instance;
        
        private static SelectSoundFactory instance
        {
            get
            {
                if (_instance == null) _instance = new SelectSoundFactory();
                return _instance;
            }
        }

        private SelectSound _selectSoundFormInstance;

        public static SelectSound SoundForm
        {
            get
            {
                if (instance._selectSoundFormInstance == null) instance._selectSoundFormInstance = new SelectSound();
                return instance._selectSoundFormInstance;
            }
        }

        private SelectSoundFactory()
        {

        }

        public static void ShowSelectSound(SelectSound.CallBack cb, SoundButtonData data)
        {
            SoundForm.UpdateSelectSound(cb, data);
            SoundForm.ShowDialog();
        }

    }
}
