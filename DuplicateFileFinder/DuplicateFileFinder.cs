using DuplicateFileFinder.DuplicatePatternMatchers;
using System.Collections.Generic;

namespace DuplicateFileFinder
{
    public class DuplicateFileFinder
    {
        private readonly IDirectoryParser directoryParser;

        public DuplicateFileFinder(IDirectoryParser directoryParser)
        {
            this.directoryParser = directoryParser;
        }

        public List<DuplicateFile> GetDuplicates(string rootDirectory, DuplicatePatternMatcher duplicatePatternMatcher)
        {
            var directories = directoryParser.FindAllDirectories(rootDirectory, IncludeRootDirectoryInResults.Yes);
            var files = directoryParser.FindAllFiles(directories);
            return duplicatePatternMatcher.FindDuplicates(files);
        }
    }
}
