namespace DuplicateFileFinder
{
    public class DuplicateFileFinder
    {
        private readonly IDirectoryParser _directoryParser;

        public DuplicateFileFinder(IDirectoryParser directoryParser)
        {
            _directoryParser = directoryParser;
        }

        public object GetDuplicates(string rootDirectory)
        {
            var directories = _directoryParser.FindAllDirectories(rootDirectory, IncludeRootDirectoryInResults.Yes);

            var files = _directoryParser.FindAllFiles(directories);

            return null;
        }
    }
}
