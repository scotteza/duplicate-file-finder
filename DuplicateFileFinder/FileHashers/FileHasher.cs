namespace DuplicateFileFinder.FileHashers
{
    public interface FileHasher
    {
        string HashFile(FileData fileData);
    }
}