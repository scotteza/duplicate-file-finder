namespace DuplicateFileFinder.FileSizers
{
    public interface FileSizer
    {
        long SizeFile(FileData fileData);
    }
}