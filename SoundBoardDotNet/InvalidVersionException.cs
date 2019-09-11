﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    public class InvalidVersionException : Exception
    {
        public InvalidVersionException()
        {
        }

        public InvalidVersionException(string message) : base(message)
        {
        }

        public InvalidVersionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}