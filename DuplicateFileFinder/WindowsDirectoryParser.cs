using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DuplicateFileFinder
{
    public class WindowsDirectoryParser : IDirectoryParser
    {
        public List<DirectoryData> FindAllDirectories(string rootDirectory, IncludeRootDirectoryInResults includeRootDirectoryInResults)
        {
            var rootDirectoryInfo = new DirectoryInfo(rootDirectory);

            var directories =
                            rootDirectoryInfo
                            .GetDirectories()
                            .Select(di => new DirectoryData(di.Name, di.FullName))
                            .ToList();

            foreach (var directory in directories.ToList())
            {
                directories.AddRange(FindAllDirectories(directory.FullPath, IncludeRootDirectoryInResults.No));
            }

            if (includeRootDirectoryInResults == IncludeRootDirectoryInResults.Yes)
            {
                directories.Add(new DirectoryData(rootDirectoryInfo.Name, rootDirectoryInfo.FullName));
            }

            return directories;
        }

        public List<FileData> FindAllFiles(DirectoryData directoryData)
        {
            var directoryInfo = new DirectoryInfo(directoryData.FullPath);
            var fileInfos = directoryInfo.GetFiles();
            return fileInfos.Select(fi => new FileData(fi.Name, fi.FullName)).ToList();
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
