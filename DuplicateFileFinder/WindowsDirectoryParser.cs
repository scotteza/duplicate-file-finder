using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DuplicateFileFinder
{
    public class WindowsDirectoryParser : IDirectoryParser
    {
        public List<DirectoryData> FindAllDirectories(string rootDirectory)
        {
            var directoryInfo = new DirectoryInfo(rootDirectory);
            var directoryInfos = directoryInfo.GetDirectories();
            var directories = directoryInfos.Select(di => new DirectoryData(di)).ToList();
            foreach (var directory in directories.ToList())
            {
                var subDirectories = FindAllDirectories(directory.FullPath);
                directories.AddRange(subDirectories);
            }
            return directories;
        }

        public List<FileData> FindAllFiles(DirectoryData directoryData)
        {
            var directoryInfo = new DirectoryInfo(directoryData.FullPath);
            var fileInfos = directoryInfo.GetFiles();
            return fileInfos.Select(fi => new FileData(fi)).ToList();
        }
    }
}
