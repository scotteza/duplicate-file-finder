using System.Collections.Generic;

namespace DuplicateFileFinder
{
    public class DuplicateFile
    {
        public object Identifier { get; }
        public int Count { get; }
        private readonly List<string> distinctFilePaths;
        public IReadOnlyCollection<string> DistinctFilePaths => distinctFilePaths.AsReadOnly();

        public DuplicateFile(object identifier, int count, List<string> distinctFilePaths)
        {
            Identifier = identifier;
            Count = count;
            this.distinctFilePaths = distinctFilePaths;
        }

        public DuplicateFile(object identifier, int count) : this(identifier, count, new List<string>())
        {
        }


        protected bool Equals(DuplicateFile other)
        {
            return Equals(Identifier, other.Identifier) && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DuplicateFile) obj);
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
