﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet.FileEncoding
{
    interface IEncodable
    {
        IEncodedValue Encode();
    }
}
