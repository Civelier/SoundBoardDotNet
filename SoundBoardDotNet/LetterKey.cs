using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    class LetterKey : SoundButtonMaker
    {
        public LetterKey(int x, int y, string name) : base(x, y, 3, 3, name)
        {
        }
    }
}
