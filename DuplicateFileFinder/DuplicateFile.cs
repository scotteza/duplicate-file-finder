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

        protected bool Equals(DuplicateFile other)
        {
            return string.Equals(Name, other.Name) && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DuplicateFile)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Count;
            }
        }
    }
}
