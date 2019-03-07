using System.Collections.Generic;
using System.Linq;

namespace DuplicateFileFinder.DuplicatePatternMatchers
{
    public class FileHashDuplicatePatternMatcher : DuplicatePatternMatcher
    {
        private readonly FileHasher fileHasher;

        public FileHashDuplicatePatternMatcher(FileHasher fileHasher)
        {
            this.fileHasher = fileHasher;
        }

        public List<DuplicateFile> FindDuplicates(List<FileData> files)
        {
            var allHashes = new Dictionary<string, int>();

            foreach (var file in files)
            {
                AddFileToHashList(allHashes, file);
            }

            return allHashes
                    .Where(x => x.Value > 1)
                    .Select(x => new DuplicateFile(x.Key, x.Value))
                    .ToList();
        }

        private void AddFileToHashList(IDictionary<string, int> allHashes, FileData file)
        {
            var hash = fileHasher.HashFile(file);

            if (!IsValidHash(hash))
            {
                return;
            }

            IncrementHashCount(allHashes, hash);
        }

        private bool IsValidHash(string hash)
        {
            return hash != null;
        }

        private void IncrementHashCount(IDictionary<string, int> allHashes, string hash)
        {
            if (!allHashes.ContainsKey(hash))
            {
                allHashes.Add(hash, 0);
            }

            allHashes[hash] += 1;
        }
    }
}