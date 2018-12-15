namespace DuplicateFileFinder
{
    public class FileFinder
    {
        private readonly IDirectoryParser _directoryParserObject;

        public FileFinder(IDirectoryParser directoryParserObject)
        {
            _directoryParserObject = directoryParserObject;
        }

        public void FindAllFiles(string rootDirectory)
        {
            var directories = _directoryParserObject.FindAllDirectories(rootDirectory);
        }
    }
}
