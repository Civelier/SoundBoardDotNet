using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet.FileEncoding
{
    class EncodedData
    {
        List<IEncodedValue> Values = new List<IEncodedValue>();

        public EncodedData()
        {

        }

        public static EncodedData operator+(EncodedData d1, EncodedData d2)
        {
            var output = new EncodedData();
            output.Values.AddRange(d1.Values);
            output.Values.AddRange(d2.Values);
            return output;
        }

        public static EncodedData operator +(EncodedData d1, IEncodedValue v1)
        {
            d1.Values.Add(v1);
            return d1;
        }
    }
}
