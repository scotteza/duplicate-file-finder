namespace DuplicateFileFinder
{
    public class DuplicateFile
    {
        public string Identifier { get; }
        public int Count { get; }

        public DuplicateFile(string identifier, int count)
        {
            Identifier = identifier;
            Count = count;
        }

        protected bool Equals(DuplicateFile other)
        {
            return string.Equals(Identifier, other.Identifier) && Count == other.Count;
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
                return ((Identifier != null ? Identifier.GetHashCode() : 0) * 397) ^ Count;
            }
        }
    }
}
