
using System;
using System.IO;

namespace CopyDirectory.Services.Wrappers
{
    public class FileWrapper : IFileWrapper
    {
        public readonly IMessageHandler _messageHandler;

        public FileWrapper(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        /// <inheritdoc cref="IFileWrapper.Copy(string, string)" />
        public void Copy(string source, string destination)
        {
            try
            {
                _messageHandler.PrintMessage($"Copying from {source} to {destination}");
                File.Copy(source, destination, true);
            }
            catch (Exception e)
            {
                _messageHandler.PrintMessage($"Unable to copy {source} to {destination} due to {e.Message}");
            }
        }
    }
}
