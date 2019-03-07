namespace DuplicateFileFinder
{
    public interface FileHasher
    {
        string HashFile(FileData fileData);
    }
}