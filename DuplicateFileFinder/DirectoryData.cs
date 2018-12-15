using System.IO;

namespace DuplicateFileFinder
{
    public class DirectoryData
    {
        public DirectoryData(DirectoryInfo di)
        {
            Name = di.Name;
            FullPath = di.FullName;
        }

        public string Name { get; set; }
        public string FullPath { get; set; }
    }
}
