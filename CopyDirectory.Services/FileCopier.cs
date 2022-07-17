using CopyDirectory.Services.Wrappers;
using CopyDirectory.Shared.Config;
using System;
using System.IO;

namespace CopyDirectory.Services
{
    public class FileCopier
    {
        private readonly IDirectoryWrapper _directoryWrapper;

        private readonly IPathWrapper _pathWrapper;

        private readonly IMessageHandler _messageHandler;

        public FileCopier(IDirectoryWrapper directoryWrapper, IPathWrapper pathWrapper, IMessageHandler messageHandler)
        {
            _directoryWrapper = directoryWrapper;
            _pathWrapper = pathWrapper;
            _messageHandler = messageHandler;
        }

        public void Execute(CLIOptions opts)
        {
            if (!_directoryWrapper.Exists(opts.DestinationPath))
            {
                _directoryWrapper.CreateDirectory(opts.DestinationPath);
            }

            var allDirectories = _directoryWrapper.GetAllDirectories(opts.SourcePath);
            foreach (string directoryPath in allDirectories)
            {
                var folderToCreate = _pathWrapper.Combine(opts.DestinationPath, directoryPath, opts.SourcePath);
                _messageHandler.PrintMessage($"Creating Folder {folderToCreate}...");
                _directoryWrapper.CreateDirectory(folderToCreate);
                _messageHandler.PrintMessage($"Created Folder {folderToCreate}...");
            }

            foreach (string filePath in _directoryWrapper.GetFiles(opts.SourcePath, allDirectories))
            {
                _messageHandler.PrintMessage($"Copying {filePath}...");
                var fileToCopy = _pathWrapper.Combine(opts.DestinationPath, filePath, opts.SourcePath);
                File.Copy(filePath, fileToCopy, true);
                _messageHandler.PrintMessage($"Copied to {fileToCopy}...");
            }
        }
    }
}
