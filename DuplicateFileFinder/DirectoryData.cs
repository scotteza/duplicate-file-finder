using System.IO;

namespace DuplicateFileFinder
{
    public class DirectoryData
    {
        public DirectoryData(DirectoryInfo di)
        {
            Name = di.Name;
        }

        public string Name { get; set; }
    }
}
