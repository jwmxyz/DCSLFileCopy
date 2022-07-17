using CopyDirectory.Shared.Utils;
using System.Collections.Generic;
using System.IO;

namespace CopyDirectory.Services.Wrappers
{
    public class DirectoryWrapper : IDirectoryWrapper
    {

        private readonly IMessageHandler _messageHandler;
        private readonly IMethodExecutionUtils _methodExecution;

        public DirectoryWrapper(IMessageHandler messageHandler, IMethodExecutionUtils methodExecution)
        {
            _messageHandler = messageHandler;
            _methodExecution = methodExecution;
        }

        ///<inheritdoc cref="IDirectoryWrapper.GetAllDirectories(string)"/>
        public IEnumerable<string> GetAllDirectories(string path)
        {
            // This would be the better option
            //return Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
            var directories = new List<string> { path };
            _messageHandler.PrintMessage($"Finding all directories in {path}");
            var subDirectories = _methodExecution.TryCatchMethod(() => Directory.GetDirectories(path), $"Unable to get Directories in {path}");
            directories.AddRange(subDirectories);
            foreach (var subDir in subDirectories)
            {
                directories.AddRange(GetAllDirectories(subDir));
            }
            return directories;
        }

        ///<inheritdoc cref="IDirectoryWrapper.CreateDirectory(string)"/>
        public DirectoryInfo CreateDirectory(string path)
        {
            return _methodExecution.TryCatchMethod(() => Directory.CreateDirectory(path), $"Unable to create directory at {path}");
        }

        ///<inheritdoc cref="IDirectoryWrapper.GetFiles(string)"/>
        public IEnumerable<string> GetFiles(string path, IEnumerable<string> directories)
        {
            // This would be the better option
            // return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            var files = new List<string>();
            foreach (string directory in directories)
            {
                _messageHandler.PrintMessage($"Finding all Files in {directory}");
                var filesInDirectory = _methodExecution.TryCatchMethod(() => Directory.GetFiles(directory), $"Unable to read all files in {directory}.");
                foreach (string file in filesInDirectory)
                {
                    files.Add(file);
                }
            }
            return files;
        }

        ///<inheritdoc cref="IDirectoryWrapper.Exists(string)"/>
        public bool Exists(string path)
        {
            return _methodExecution.TryCatchMethod(() => Directory.Exists(path), $"Unable to check if {path} exists.");
        }
    }
}
