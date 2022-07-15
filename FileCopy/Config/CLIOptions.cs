using CommandLine;
using System;

namespace FileCopy.Config
{
    public class CLIOptions
    {
        [Option('s', "source", Required = true, HelpText = "The source directory path of where to copy from")]
        public Uri Source { private get; set; }

        [Option('d', "destination", Required = true, HelpText = "The destination directory of where to copy the files")]
        public Uri Destination { private get; set; }

        public string SourcePath => Source.LocalPath;

        public string DestinationPath => Destination.LocalPath;

        public bool IsValid()
        {
            return !Source.AbsolutePath.Equals(Destination.AbsolutePath, StringComparison.OrdinalIgnoreCase);
        }
    }
}
