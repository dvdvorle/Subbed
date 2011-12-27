using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DvdV.Subbed.Core.Formats
{
    public class SubtitleManager
    {
        public SubtitleManager(IEnumerable<ISubtitle> subtitles)
        {
            Subtitles = subtitles.ToList();
        }

        public List<ISubtitle> Subtitles { get; private set; }

        public void TransposeBy(TimeSpan diff)
        {
            foreach (var sub in Subtitles)
            {
                sub.TransposeBy(diff);
            }
        }

        public void StretchBy(double factor)
        {
            foreach (var sub in Subtitles)
            {
                sub.TransposeTo(new TimeSpan((long)(sub.Begin.Ticks * factor)));
                sub.StretchBy(factor);
            }
        }

        public void Extrapolate(ISubtitle targetSub, TimeSpan diff)
        {
            double multiplicationFactor = (targetSub.Begin.Ticks + diff.Ticks) / (double)targetSub.Begin.Ticks;
            StretchBy(multiplicationFactor);
        }
    }
}
