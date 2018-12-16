using System.IO;

namespace DuplicateFileFinder
{
    public class DirectoryData
    {
        public string Name { get; set; }
        public string FullPath { get; set; }

        public DirectoryData(DirectoryInfo di)
        {
            Name = di.Name;
            FullPath = di.FullName;
        }

        public override string ToString()
        {
            return $"{Name} - {FullPath}";
        }
    }
}
