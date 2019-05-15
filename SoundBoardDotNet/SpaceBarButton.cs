using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    class SpaceBarButton : SoundButtonMaker
    {
        public SpaceBarButton(int x, int y, int height, int width) : base(x, y, height, width, "Space (stop all)")
        {
        }

        protected override void Btn_Click(object sender, EventArgs e)
        {
        }
    }
}
