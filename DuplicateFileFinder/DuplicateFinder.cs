namespace DuplicateFileFinder
{
    public class DuplicateFinder
    {
        private readonly IDirectoryParser _directoryParserObject;

        public DuplicateFinder(IDirectoryParser directoryParserObject)
        {
            _directoryParserObject = directoryParserObject;
        }

        public object FindAllFiles()
        {
            throw new System.NotImplementedException();
        }
    }
}
