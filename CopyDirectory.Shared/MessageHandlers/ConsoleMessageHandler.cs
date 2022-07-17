using System;
using System.Collections.Generic;
using System.Text;

namespace CopyDirectory.Services
{
    public class ConsoleMessageHandler : IMessageHandler
    {
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
