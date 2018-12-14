namespace DuplicateFileFinder
{
    public class FileFinder
    {
        private readonly IDirectoryParser _directoryParserObject;

        public FileFinder(IDirectoryParser directoryParserObject)
        {
            _directoryParserObject = directoryParserObject;
        }

        public object FindAllFiles(string rootDirectory)
        {
            return _directoryParserObject.FindAllFiles(rootDirectory);
        }
    }
}
