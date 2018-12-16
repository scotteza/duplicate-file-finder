namespace DuplicateFileFinder
{
    public class DirectoryData
    {
        public string Name { get; }
        public string FullPath { get; }

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
