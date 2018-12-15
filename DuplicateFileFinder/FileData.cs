using System.IO;

namespace DuplicateFileFinder
{
    public class FileData
    {
        public FileData(FileInfo fi)
        {
            Name = fi.Name;
        }

        public string Name { get; set; }
    }
}