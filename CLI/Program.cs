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

                var parserFactory = new SubtitleParserFactory();
                var reader = parserFactory.GetReaderFor(options.InputFile);
                var subs = reader.Read(options.InputFile);
                
                var subManager = new SubtitleManager(subs);

                switch (options.ActionVerb)
                {
                    case Verbs.Extrapolate:
                        var targetSub = subs.FirstOrDefault(options.Filter);
                        subManager.Extrapolate(targetSub, options.Target);
                        break;
                    case Verbs.StretchBy:
                        subManager.StretchBy(options.Factor);
                        break;
                    case Verbs.TransposeBy:
                        subManager.TransposeBy(options.Target);
                        break;
                    default:
                        return;
                }

                subs = subManager.Subtitles;
        
                var writer = parserFactory.GetWriterFor(options.OutputFile);
                writer.Write(subs, options.OutputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
