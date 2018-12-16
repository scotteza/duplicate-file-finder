using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DuplicateFileFinder
{
    public class WindowsDirectoryParser : IDirectoryParser
    {
        public List<DirectoryData> FindAllDirectories(string rootDirectory)
        {
            var directories =
                new DirectoryInfo(rootDirectory)
                    .GetDirectories()
                    .Select(di => new DirectoryData(di))
                    .ToList();

            foreach (var directory in directories.ToList())
            {
                directories.AddRange(FindAllDirectories(directory.FullPath));
            }

            return directories;
        }

        public List<FileData> FindAllFiles(DirectoryData directoryData)
        {
            var directoryInfo = new DirectoryInfo(directoryData.FullPath);
            var fileInfos = directoryInfo.GetFiles();
            return fileInfos.Select(fi => new FileData(fi)).ToList();
        }

        public List<FileData> FindAllFiles(IEnumerable<DirectoryData> directories)
        {
            var files = new List<FileData>();

            foreach (var directory in directories)
            {
                files.AddRange(FindAllFiles(directory));
            }

            return files;
        }
    }
}
