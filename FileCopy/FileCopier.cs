using FileCopy.Config;
using FileCopy.Wrappers;
using System;
using System.IO;

namespace FileCopy
{
    public class FileCopier
    {
        private readonly IDirectoryWrapper _directoryWrapper;

        private readonly IPathWrapper _pathWrapper;

        public FileCopier(IDirectoryWrapper directoryWrapper, IPathWrapper pathWrapper)
        {
            _directoryWrapper = directoryWrapper;
            _pathWrapper = pathWrapper;
        }

        public void Execute(CLIOptions opts)
        {
            if (!opts.IsValid())
            {
                throw new Exception("Source and destination cannot be the same path");
            }

            if (!_directoryWrapper.Exists(opts.DestinationPath))
            {
                _directoryWrapper.CreateDirectory(opts.DestinationPath);
            }

            foreach (string directoryPath in _directoryWrapper.GetAllDirectories(opts.SourcePath))
            {
                _directoryWrapper.CreateDirectory(_pathWrapper.Combine(opts.DestinationPath, directoryPath, opts.SourcePath));
            }

            foreach (string filePath in _directoryWrapper.GetFiles(opts.SourcePath))
            {
               
                File.Copy(filePath, _pathWrapper.Combine(opts.DestinationPath, filePath, opts.SourcePath), true);
            }
        }
    }
}
