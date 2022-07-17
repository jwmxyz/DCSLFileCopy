using System.Collections.Generic;
using System.IO;

namespace CopyDirectory.Services.Wrappers
{
    /// <summary>
    /// A wrapper class for the <cref="System.IO.Directory" /> class. 
    /// Enables dependency inject with better mocking and testability.
    /// </summary>
    public interface IDirectoryWrapper
    {
        /// <summary>
        /// Method used to create a new Directory
        /// </summary>
        /// <param name="path">The path to where the directory should be created</param>
        /// <returns>The directory info of the newly created Directory.</returns>
        DirectoryInfo CreateDirectory(string path);

        /// <summary>
        /// Obtains all of the directories within a directory.
        /// </summary>
        /// <param name="path">The path to the directory we want to search</param>
        /// <returns>A string array of all the paths to all the directoryies under the path folder</returns>
        IEnumerable<string> GetAllDirectories(string path);

        /// <summary>
        /// Obtains all of the files under all of the directories
        /// </summary>
        /// <param name="path">The path to the root folder</param>
        /// <returns>A String array of all the paths to each of the files</returns>
        IEnumerable<string> GetFiles(string path, IEnumerable<string> directories);

        /// <summary>
        /// Checks to see if a directory already exists.
        /// </summary>
        /// <param name="path">The path to check to see if a directory exists.</param>
        /// <returns>true if its a directory false otherwise.</returns>
        bool Exists(string path);
    }
}