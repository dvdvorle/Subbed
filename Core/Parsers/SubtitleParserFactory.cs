using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DvdV.Subbed.Core.Parsers
{
    public class SubtitleParserFactory
    {
        /// <summary>
        /// Uses the extension of the filename to return the appropiate parser.
        /// </summary>
        /// <param name="inputFileName">A subtitle file</param>
        /// <returns>A parser for the inputFileName</returns>
        public ISubtitleParser GetReaderFor(string inputFileName)
        {
            return GetParserFor(inputFileName);
        }

        /// <summary>
        /// Uses the extension of the filename to return the appropiate parser.
        /// </summary>
        /// <param name="outputFileName">A subtitle file</param>
        /// <returns>A parser for the outputFileName</returns>
        public ISubtitleParser GetWriterFor(string outputFileName)
        {
            return GetParserFor(outputFileName);
        }

        /// <summary>
        /// Uses the extension of the filename to return the appropiate parser.
        /// </summary>
        /// <param name="fileName">A subtitle file</param>
        /// <returns>A parser for the fileName</returns>
        public ISubtitleParser GetParserFor(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            switch (extension)
            {
                case ".srt":
                    return new SubRipParser();
                default:
                    throw new UnsupportedSubtitleFormatException(fileName);
            }
        }
    }
}
