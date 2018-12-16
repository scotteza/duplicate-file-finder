using System.IO;

namespace DuplicateFileFinder
{
    public class DirectoryData
    {
        public string Name { get; private set; }
        public string FullPath { get; private set; }

        public DirectoryData(DirectoryInfo di)
        {
            Name = di.Name;
            FullPath = di.FullName;
        }

        public DirectoryData(string name, string fullPath)
        {
            Name = name;
            FullPath = fullPath;
        }


        public override string ToString()
        {
            return $"{Name} - {FullPath}";
        }
    }
}
