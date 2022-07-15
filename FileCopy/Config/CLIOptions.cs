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

        /// <summary>
        /// Used to obtain the local path string of the source
        /// </summary>
        public string SourcePath => Source.LocalPath;

        /// <summary>
        /// Used to get the local path string of the destination
        /// </summary>
        public string DestinationPath => Destination.LocalPath;

        /// <summary>
        /// Used to check the the imputted strings are not the same
        /// </summary>
        /// <returns>true if the strings dont match false if they are equal.</returns>
        public bool IsValid()
        {
            return !Source.AbsolutePath.Equals(Destination.AbsolutePath, StringComparison.OrdinalIgnoreCase);
        }
    }
}
