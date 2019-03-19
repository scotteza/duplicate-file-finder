using System.IO;

namespace DuplicateFileFinder.FileSizers
{
    public class WindowsFileSizer : FileSizer
    {
        public long SizeFile(FileData fileData)
        {
            var fileInfo = new FileInfo(fileData.FullName);
            return fileInfo.Length;
        }
    }
}