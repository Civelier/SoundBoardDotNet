using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet.FileEncoding
{
    abstract class IEncodedValue
    {
        public string Name;
        public Type MyType;

        public IEncodedValue(IEncodable value)
        {

        }
    }

    abstract class IEncodedValue<T> : IDecodable<T>
    {
        public string Name;
        public Type MyType;

        public abstract T Decode();

        public T Decode(EncodedData data)
        {
            throw new NotImplementedException();
        }
    }

    
}
