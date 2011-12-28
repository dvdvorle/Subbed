using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DvdV.Subbed.Core.Formats;

namespace DvdV.Subbed.Core.Parsers
{
    public class SubRipParser : ISubtitleParser
    {
        public void Write(IEnumerable<ISubtitle> subtitles, string output)
        {
            List<string> lines = new List<string>();

            int counter = 1;

            foreach (var sub in subtitles)
            {
                if(counter > 1)
                {
                    lines.Add("");
                }

                lines.Add(counter.ToString());
                lines.Add(String.Format("{0:d2}:{1:d2}:{2:d2},{3:d3} --> {4:d2}:{5:d2}:{6:d2},{7:d3}",
                            sub.Begin.Hours, sub.Begin.Minutes, sub.Begin.Seconds, sub.Begin.Milliseconds,
                            sub.End.Hours, sub.End.Minutes, sub.End.Seconds, sub.End.Milliseconds));

                string[] textLines = sub.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var textLine in textLines)
                {
                    lines.Add(textLine);
                }
                counter++;
            }

            File.WriteAllLines(output, lines.ToArray());
        }

        IEnumerable<ISubtitle> ISubtitleParser.Read(string input)
        {
            return Read(input);
        }

        public IEnumerable<BaseBeginEndSubtitle> Read(string input)
        {
            string[] lines = File.ReadAllLines(input);
            List<BaseBeginEndSubtitle> subs = new List<BaseBeginEndSubtitle>();

            SubRipStatus status = SubRipStatus.ParseNumber;
            TimeSpan begin = new TimeSpan();
            TimeSpan end = new TimeSpan();
            string text = "";

            for (int i = 0; i < lines.Length; i++)
            {
                switch (status)
                {
                    case SubRipStatus.ParseNumber:
                        int ignore;
                        if(int.TryParse(lines[i], out ignore))
                        {
                            status = SubRipStatus.ParseTime;
                        }
                        break;
                    case SubRipStatus.ParseTime:
                        begin = GetBegin(lines[i]);
                        end = GetEnd(lines[i]);
                        status = SubRipStatus.ParseText;
                        break;
                    case SubRipStatus.ParseText:
                        if (!String.IsNullOrEmpty(lines[i]))
                        {
                            if(!String.IsNullOrEmpty(text))
                            {
                                text += "\r\n";
                            }

                            text += lines[i];
                        }
                        else
                        {
                            subs.Add(new BaseBeginEndSubtitle(begin, end, text));
                            begin = new TimeSpan();
                            end = new TimeSpan();
                            text = "";
                            status = SubRipStatus.ParseNumber;
                        }
                        break;
                }
            }

            if(status == SubRipStatus.ParseText)
            {
                subs.Add(new BaseBeginEndSubtitle(begin, end, text));
            }

            return subs;
        }

        internal TimeSpan GetBegin(string line)
        {
            string beginText = line.Split(' ')[0];
            string[] beginTokens = beginText.Split(new char[] { ':', ',' });
            return new TimeSpan(0, int.Parse(beginTokens[0]), int.Parse(beginTokens[1]), int.Parse(beginTokens[2]), int.Parse(beginTokens[3]));
        }

        internal TimeSpan GetEnd(string line)
        {
            string endText = line.Split(' ')[2];
            string[] endTokens = endText.Split(new char[] { ':', ',' });
            return new TimeSpan(0, int.Parse(endTokens[0]), int.Parse(endTokens[1]), int.Parse(endTokens[2]), int.Parse(endTokens[3]));
        }

        enum SubRipStatus
        {
            ParseNumber,
            ParseTime,
            ParseText
        }
    }

    
}
