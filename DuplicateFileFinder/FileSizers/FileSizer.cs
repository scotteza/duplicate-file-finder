namespace DuplicateFileFinder.FileSizers
{
    public interface FileSizer
    {
        int GetFileSize(FileData file1);
    }
}