using System.IO;

namespace FileCopy.Wrappers
{
    internal class DirectoryWrapper : IDirectoryWrapper
    {
        ///<inheritdoc cref="IDirectoryWrapper.GetAllDirectories(string)"/>
        public string[] GetAllDirectories(string path)
        {
            return Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
        }

        ///<inheritdoc cref="IDirectoryWrapper.CreateDirectory(string)"/>
        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        ///<inheritdoc cref="IDirectoryWrapper.GetFiles(string)"/>
        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        }

        ///<inheritdoc cref="IDirectoryWrapper.Exists(string)"/>
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
