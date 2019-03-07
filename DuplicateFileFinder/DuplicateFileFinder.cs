using DuplicateFileFinder.DuplicatePatternMatchers;
using System.Collections.Generic;

namespace DuplicateFileFinder
{
    public class DuplicateFileFinder
    {
        private readonly IDirectoryParser _directoryParser;

        public DuplicateFileFinder(IDirectoryParser directoryParser)
        {
            _directoryParser = directoryParser;
        }

        public List<DuplicateFile> GetDuplicates(string rootDirectory, DuplicatePatternMatcher duplicatePatternMatcher)
        {
            var directories = _directoryParser.FindAllDirectories(rootDirectory, IncludeRootDirectoryInResults.Yes);
            var files = _directoryParser.FindAllFiles(directories);
            return duplicatePatternMatcher.FindDuplicates(files);
        }
    }
}
