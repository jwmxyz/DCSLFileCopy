using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CopyDirectory.Services.Wrappers
{
    public class DirectoryWrapper : IDirectoryWrapper
    {
        ///<inheritdoc cref="IDirectoryWrapper.GetAllDirectories(string)"/>
        public IEnumerable<string> GetAllDirectories(string path, List<string> directories = null)
        {
            // This would be the better option
            //return Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
            directories = directories ?? new List<string> { path };

            var subDirectories = Directory.GetDirectories(path).ToList();
            foreach (var subDir in subDirectories)
            {
                directories.AddRange(GetAllDirectories(subDir));
            }
            directories.AddRange(subDirectories);
            return directories;
        }

        ///<inheritdoc cref="IDirectoryWrapper.CreateDirectory(string)"/>
        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        ///<inheritdoc cref="IDirectoryWrapper.GetFiles(string)"/>
        public IEnumerable<string> GetFiles(string path, IEnumerable<string> directories, List<string> files = null)
        {
            // This would be the better option
            // return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            files = files ?? new List<string>();
            foreach (string directory in directories)
            {
                foreach (string file in Directory.GetFiles(directory))
                {
                    files.Add(file);
                }
            }
            return files;
        }

        ///<inheritdoc cref="IDirectoryWrapper.Exists(string)"/>
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }
    }
}
