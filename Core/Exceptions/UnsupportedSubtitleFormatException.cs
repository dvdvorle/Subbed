using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DvdV.Subbed.Core
{
    [Serializable]
    public class UnsupportedSubtitleFormatException : Exception
    {
        public UnsupportedSubtitleFormatException() { }
        public UnsupportedSubtitleFormatException(string fileName) : base(fileName) { }

        protected UnsupportedSubtitleFormatException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
