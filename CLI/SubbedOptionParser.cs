using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;
using DvdV.Subbed.Core.Formats;
using System.IO;

namespace DvdV.Subbed.CLI
{
    class SubbedOptionParser
    {
        private OptionSet _options;

        public SubbedOptionParser(string[] args)
        {
            InitProperties();
            InitOptionSet();

            Parse(args);
        }

        public Verbs ActionVerb { get; private set; }
        public TimeSpan TimeDiff { get; private set; }
        public Func<ISubtitle, bool> SubtitleSelector { get; private set; }
        public double Factor { get; private set; }
        public string InputFile { get; private set; }
        public string OutputFile { get; private set; }

        public void WriteHelp()
        {
            Console.WriteLine("Usage: CLI.exe <inputfile> [outputfile] <args>");
            Console.WriteLine();
            Console.WriteLine("Where <args> is one of:");
            _options.WriteOptionDescriptions(Console.Out);
        }

        private void InitProperties()
        {
            ActionVerb = Verbs.Help;
            TimeDiff = TimeSpan.FromSeconds(0);
            Factor = 0;
            SubtitleSelector = s => true;
        }


        private void InitOptionSet()
        {
            _options = new OptionSet()
                {
                    { "s|stretchby=", "",
                        (double v) => 
                            { 
                                ActionVerb = Verbs.StretchBy;
                                Factor = v;
                            }
                    },
                    { "t|transposeby=",
                        (double v) =>
                            {
                                ActionVerb = Verbs.TransposeBy;
                                TimeDiff = TimeSpan.FromSeconds(v);
                            }
                    },
                    { "e|extrapolate={+}",
                        (string t, double v) =>
                            {
                                ActionVerb = Verbs.Extrapolate;
                                SubtitleSelector = s => s.Text.StartsWith(t);
                                TimeDiff = TimeSpan.FromSeconds(v);
                            }
                    },
                    { "h|help",
                        v => ActionVerb = Verbs.Help
                    }
                };
        }

        private void Parse(string[] args)
        {
            List<string> fileNames = _options.Parse(args);

            if (ActionVerb != Verbs.Help)
            {
                if (fileNames.Count() < 1 || fileNames.Count() > 2)
                {
                    throw new ArgumentException("Provide 1 or 2 filenames");
                }

                InputFile = fileNames[0];
                CheckIfInputFileExists();

                bool outputSameAsInput = fileNames.Count() > 1;
                OutputFile = outputSameAsInput ? fileNames[1] : fileNames[0];
            }
        }

        private void CheckIfInputFileExists()
        {
            if (!File.Exists(InputFile))
            {
                throw new FileNotFoundException("Inputfile not found", InputFile);
            }
        }
    }
}
