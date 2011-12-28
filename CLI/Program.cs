using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;
using DvdV.Subbed.Core.Formats;
using System.Diagnostics;
using DvdV.Subbed.Core.Parsers;
using System.IO;

namespace DvdV.Subbed.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var options = new SubbedOptionParser(args);
                if (options.ActionVerb == Verbs.Help)
                {
                    options.WriteHelp();
                    return;
                }

                var parserFactory = new SubtitleParserFactory();
                ISubtitleParser reader = parserFactory.GetReaderFor(options.InputFile);
                IEnumerable<ISubtitle> subs = reader.Read(options.InputFile);
                
                var subManager = new SubtitleManager(subs);

                switch (options.ActionVerb)
                {
                    case Verbs.Extrapolate:
                        ISubtitle targetSub = subs.FirstOrDefault(options.SubtitleSelector);
                        subManager.Extrapolate(targetSub, options.TimeDiff);
                        break;
                    case Verbs.StretchBy:
                        subManager.StretchBy(options.Factor);
                        break;
                    case Verbs.TransposeBy:
                        subManager.TransposeBy(options.TimeDiff);
                        break;
                }

                subs = subManager.Subtitles;
        
                ISubtitleParser writer = parserFactory.GetWriterFor(options.OutputFile);
                writer.Write(subs, options.OutputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
