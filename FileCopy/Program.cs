using CommandLine;
using FileCopy.Config;
using FileCopy.Wrappers;
using System;
using System.IO;

namespace FileCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CLIOptions>(args)
                   .WithParsed(RunFileCopy);
        }

        static void RunFileCopy(CLIOptions opts)
        {
            if (!opts.IsValid())
            {
                throw new Exception("Source and destination cannot be the same path");
            }

            if (!Directory.Exists(opts.DestinationPath))
            {
                DirectoryWrapper.CreateDirectory(opts.DestinationPath);
            }

            foreach (string dirPath in DirectoryWrapper.GetDirectories(opts.SourcePath, "*", SearchOption.AllDirectories))
            DirectoryWrapper.CreateDirectory(Path.Combine(opts.DestinationPath, dirPath.Remove(0, opts.SourcePath.Length)));

            foreach (string newPath in DirectoryWrapper.GetFiles(opts.SourcePath, "*.*", SearchOption.AllDirectories))
            {
                var destinationPath = Path.Combine(opts.DestinationPath, newPath.Remove(0, opts.SourcePath.Length));
                File.Copy(newPath, Path.Combine(opts.DestinationPath, newPath.Remove(0, opts.SourcePath.Length)), true);
            }
        }
    }
}
