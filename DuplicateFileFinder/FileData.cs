using System.IO;

namespace DuplicateFileFinder
{
    public class FileData
    {
        public string Name { get; private set; }

        public FileData(FileInfo fi)
        {
            Name = fi.Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}