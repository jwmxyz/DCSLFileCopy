namespace CopyDirectory.Services
{
    public interface IMessageHandler
    {
        /// <summary>
        /// Method that hand printing a message
        /// </summary>
        /// <param name="message">the message to print</param>
        void PrintMessage(string message);

        /// <summary>
        /// Method that will handle printing an error message
        /// </summary>
        /// <param name="error">the error message to print</param>
        void PrintError(string error);
    }
}