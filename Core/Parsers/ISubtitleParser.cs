using System;
using System.Collections.Generic;
using DvdV.Subbed.Core.Formats;

namespace DvdV.Subbed.Core.Parsers
{
    public interface ISubtitleParser
    {
        IEnumerable<ISubtitle> Read(string input);
        void Write(IEnumerable<ISubtitle> subtitles, string output);
    }
}
