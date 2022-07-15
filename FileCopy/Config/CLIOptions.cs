using CommandLine;
using FileCopy.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace FileCopy.Config
{
    public class CLIOptions : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Source.AbsolutePath.Equals(Destination.AbsolutePath, StringComparison.OrdinalIgnoreCase))
            {
                yield return new ValidationResult("Source uri cannot be the same as destination uri.");
            }

            if (!Directory.Exists(SourcePath))
            {
                yield return new ValidationResult("The source directory does not exists.");
            }
        }
    }
}
