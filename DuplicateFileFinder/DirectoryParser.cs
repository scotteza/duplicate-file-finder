using System.Collections.Generic;

namespace DuplicateFileFinder
{
    public interface IDirectoryParser
    {
        List<DirectoryData> FindAllDirectories(string rootDirectory, bool includeRootDirectoryInResults);
        List<FileData> FindAllFiles(DirectoryData directoryData);
        List<FileData> FindAllFiles(IEnumerable<DirectoryData> directories);
    }
}
