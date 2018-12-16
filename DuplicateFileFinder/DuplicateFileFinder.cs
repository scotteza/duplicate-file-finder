using System.Collections.Generic;
using System.Linq;

namespace DuplicateFileFinder
{
    public class DuplicateFileFinder
    {
        private readonly IDirectoryParser _directoryParser;

        public DuplicateFileFinder(IDirectoryParser directoryParser)
        {
            _directoryParser = directoryParser;
        }

        public List<DuplicateFile> GetDuplicates(string rootDirectory)
        {
            List<DuplicateFile> result = new List<DuplicateFile>();

            var directories = _directoryParser.FindAllDirectories(rootDirectory, IncludeRootDirectoryInResults.Yes);
            var files = _directoryParser.FindAllFiles(directories);

            var distinctFileNames = files.Select(f => f.Name).Distinct();
            foreach (var distinctFileName in distinctFileNames)
            {
                var fileCount = files.Count(f => f.Name == distinctFileName);
                if (fileCount > 1)
                {
                    result.Add(new DuplicateFile(distinctFileName, fileCount));
                }
            }

            return result;
        }
    }
}
