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
                var parserFactory = new SubtitleParserFactory();
                Verbs todo = 0;
                TimeSpan target = TimeSpan.FromSeconds(0);
                Func<ISubtitle, bool> filter = s => true;
                double factor = 0;

                var p = new OptionSet()
                {
                    { "s|stretchby=",
                        (double v) => 
                            { 
                                todo = Verbs.StretchBy;
                                factor = v;
                            } },
                    { "t|transposeby=",
                        (double v) =>
                            {
                                todo = Verbs.TransposeBy;
                                target = TimeSpan.FromSeconds(v);
                            } },
                    { "e|extrapolate={+}",
                        (string t,double v) =>
                            {
                                todo = Verbs.Extrapolate;
                                filter = s => s.Text.StartsWith(t);
                                target = TimeSpan.FromSeconds(v);
                            } }
                };

                var fileNames = p.Parse(args);

                if (fileNames.Count() < 1 || fileNames.Count() > 2)
                {
                    throw new Exception("Provide 1 or 2 filenames");
                }
              
                string inputFile = fileNames[0];

                bool outputSameAsInput = fileNames.Count() > 1;
                string outputFile = outputSameAsInput ? fileNames[1] : fileNames[0];

                var reader = parserFactory.GetReaderFor(inputFile);
                var subs = reader.Read(inputFile);
                var man = new Subbed.Core.Formats.SubtitleManager(subs);

                switch (todo)
                {
                    case Verbs.Extrapolate:
                        var targetSub = subs.FirstOrDefault(filter);
                        man.Extrapolate(targetSub, target);

                        break;
                    case Verbs.StretchBy:
                        man.StretchBy(factor);

                        break;
                    case Verbs.TransposeBy:
                        man.TransposeBy(target);

                        break;
                    default:
                        Console.WriteLine("Usage: CLI.exe <inputfile> [outputfile] <args>");
                        Console.WriteLine();
                        Console.WriteLine("Where <args> is one of:");
                        p.WriteOptionDescriptions(Console.Out);
                        return;
                }

                subs = man.Subtitles;
        
                var writer = parserFactory.GetWriterFor(outputFile);
                writer.Write(subs, outputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
