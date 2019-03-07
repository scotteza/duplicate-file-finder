using System.Collections.Generic;
using System.Linq;

namespace DuplicateFileFinder
{
    public class FileNameDuplicatePatternMatcher : DuplicatePatternMatcher
    {
        public List<DuplicateFile> FindDuplicates(List<FileData> files)
        {
            var result = new List<DuplicateFile>();

            var distinctFileNames = files.Select(f => f.Name).Distinct();
            foreach (var distinctFileName in distinctFileNames)
            {
                var fileCount = files.Count(f => f.Name == distinctFileName);
                if (fileCount > 1)
                {
                    result.Add(new DuplicateFile(distinctFileName, fileCount));
                }
            }

            return result;
        }
    }
}