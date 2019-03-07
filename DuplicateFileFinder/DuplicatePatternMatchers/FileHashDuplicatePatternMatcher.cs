using System.Collections.Generic;
using System.Linq;
using DuplicateFileFinder.FileHashers;

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
            var allHashes = new Dictionary<string, List<FileData>>();

            foreach (var file in files)
            {
                AddFileToHashList(allHashes, file);
            }

            return allHashes
                    .Where(x => x.Value.Count > 1)
                    .Select(x => new DuplicateFile(x.Key, x.Value.Count, x.Value.Select(f=>f.FullName).ToList()))
                    .ToList();
        }

        private void AddFileToHashList(IDictionary<string, List<FileData>> allHashes, FileData file)
        {
            var hash = fileHasher.HashFile(file);

            if (IsInvalidValidHash(hash))
            {
                return;
            }

            if (!allHashes.ContainsKey(hash))
            {
                allHashes.Add(hash, new List<FileData>());
            }

            allHashes[hash].Add(file);
        }

        private bool IsInvalidValidHash(string hash)
        {
            return hash == null;
        }
    }
}