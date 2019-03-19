using DuplicateFileFinder.FileSizers;
using System.Collections.Generic;
using System.Linq;

namespace DuplicateFileFinder.DuplicatePatternMatchers
{
    public class FileSizeDuplicatePatternMatcher : DuplicatePatternMatcher
    {
        private readonly FileSizer fileSizer;

        public FileSizeDuplicatePatternMatcher(FileSizer fileSizer)
        {
            this.fileSizer = fileSizer;
        }

        public List<DuplicateFile> FindDuplicates(List<FileData> files)
        {
            var allSizes = new Dictionary<long, List<FileData>>();

            foreach (var file in files)
            {
                AddFileToSizeList(allSizes, file);
            }

            return allSizes
                .Where(x => x.Value.Count > 1)
                .Select(x => new DuplicateFile(x.Key, x.Value.Count, x.Value.Select(f => f.FullName).ToList()))
                .ToList();
        }

        private void AddFileToSizeList(IDictionary<long, List<FileData>> allSizes, FileData file)
        {
            var size = fileSizer.SizeFile(file);

            if (!allSizes.ContainsKey(size))
            {
                allSizes.Add(size, new List<FileData>());
            }

            allSizes[size].Add(file);
        }
    }
}