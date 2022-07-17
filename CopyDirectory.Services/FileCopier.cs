using CopyDirectory.Services.Wrappers;
using CopyDirectory.Shared.Config;
using System;
using System.IO;
using System.Linq;

namespace CopyDirectory.Services
{
    public class FileCopier
    {
        private readonly IDirectoryWrapper _directoryWrapper;
        private readonly IPathWrapper _pathWrapper;
        private readonly IMessageHandler _messageHandler;
        private readonly IFileWrapper _fileWrapper;

        public FileCopier(IDirectoryWrapper directoryWrapper, IPathWrapper pathWrapper, IMessageHandler messageHandler, IFileWrapper fileWrapper)
        {
            _directoryWrapper = directoryWrapper;
            _pathWrapper = pathWrapper;
            _messageHandler = messageHandler;
            _fileWrapper = fileWrapper;
        }

        public void Execute(CLIOptions opts)
        {
            if (!_directoryWrapper.Exists(opts.DestinationPath))
            {
                _directoryWrapper.CreateDirectory(opts.DestinationPath);
            }

            var allDirectories = _directoryWrapper.GetAllDirectories(opts.SourcePath);
            //we skip the first directory as that does not need creating.
            foreach (string directoryPath in allDirectories.Skip(1))
            {
                var folderToCreate = _pathWrapper.Combine(opts.DestinationPath, directoryPath, opts.SourcePath);
                _messageHandler.PrintMessage($"Creating Folder {folderToCreate}...");
                _directoryWrapper.CreateDirectory(folderToCreate);
                _messageHandler.PrintMessage($"Created Folder {folderToCreate}...");
            }

            foreach (string fileSource in _directoryWrapper.GetFiles(opts.SourcePath, allDirectories))
            {
                _fileWrapper.Copy(fileSource, _pathWrapper.Combine(opts.DestinationPath, fileSource, opts.SourcePath));
            }
        }
    }
}
