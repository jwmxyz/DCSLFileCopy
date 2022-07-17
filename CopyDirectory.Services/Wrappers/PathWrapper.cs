using System.IO;

namespace CopyDirectory.Services.Wrappers
{
    public class PathWrapper : IPathWrapper
    {
        ///<inheritdoc cref="IPathWrapper.Combine(string, string, string)" />
        public string Combine(string destinationPath, string filePath, string rootDirectory)
        {
            if (rootDirectory.EndsWith("\\"))
            {
                rootDirectory = rootDirectory.Substring(0, rootDirectory.Length - 1);
            }
            // We need to +1 on the original source length to remove the leading `/`
            // Microsoft otherwise class it as a Rooted path and return the path2 variable instead of actually combining
            // https://referencesource.microsoft.com/#mscorlib/system/io/path.cs,1295 -> https://referencesource.microsoft.com/#mscorlib/system/io/path.cs,1186
            // ... Classic Microsoft ...
            return Path.Combine(destinationPath, filePath.Remove(0, rootDirectory.Length + 1));
        }
    }
}
