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
            return directoryInfos.Select(di => new DirectoryData(di)).ToList();
        }
    }
}
