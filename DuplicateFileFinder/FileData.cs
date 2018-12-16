namespace DuplicateFileFinder
{
    public class FileData
    {
        public string Name { get; }

        public FileData(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
