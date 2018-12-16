namespace DuplicateFileFinder
{
    public class DuplicateFile
    {
        public string Name { get; }
        public int Count { get; }

        public DuplicateFile(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
