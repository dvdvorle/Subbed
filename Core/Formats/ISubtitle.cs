using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DvdV.Subbed.Core.Formats
{
    public interface ISubtitle
    {
        TimeSpan Duration { get; }
        TimeSpan Begin { get; }
        TimeSpan End { get; }
        string Text { get; set; }

        void TransposeBy(TimeSpan diff);
        void TransposeTo(TimeSpan begin);
        void TransposeTill(TimeSpan end);

        void StretchBy(double factor);
        void StretchTo(TimeSpan duration);
    }
}
