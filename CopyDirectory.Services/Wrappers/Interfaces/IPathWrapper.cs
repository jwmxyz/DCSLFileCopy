namespace CopyDirectory.Services.Wrappers
{
    public interface IPathWrapper
    {
        /// <summary>
        /// Method that will combine two paths into one, trimming the original root directory from the file path.
        /// See code comments inside the method 
        /// </summary>
        /// <param name="destinationPath">the destination path - a folder to place the file</param>
        /// <param name="filePath">The file path before moving therefore including the root directory</param>
        /// <param name="rootDirectory">The root directory that should be taken from the filePath</param>
        /// <returns>The newly combined path where the file should be placed.</returns>
        string Combine(string destinationPath, string filePath, string rootDirectory);
    }
}