using System;

namespace CopyDirectory.Services
{
    public class ConsoleMessageHandler : IMessageHandler
    {
        /// <inheritdoc cref="IMessageHandler.PrintError(string)"/>
        public void PrintError(string error)
        {
            Console.Error.WriteLine(error);
        }

        /// <inheritdoc cref="IMessageHandler.PrintMessage(string)"/>
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
