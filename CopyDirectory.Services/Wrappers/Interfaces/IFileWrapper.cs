namespace CopyDirectory.Services.Wrappers
{
    public interface IFileWrapper
    {
        /// <summary>
        /// Method used to copy the file from one place to another
        /// </summary>
        /// <param name="source">The source of the file</param>
        /// <param name="destination">The destination of where to copy the file to</param>
        void Copy(string source, string destination);
    }
}