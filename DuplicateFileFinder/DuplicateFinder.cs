namespace DuplicateFileFinder
{
    public class DuplicateFinder
    {
        private readonly IDirectoryParser _directoryParserObject;

        public DuplicateFinder(IDirectoryParser directoryParserObject)
        {
            _directoryParserObject = directoryParserObject;
        }

        public object FindAllFiles(string rootDirectory)
        {
            return _directoryParserObject.FindAllFiles(rootDirectory);
        }
    }
}
