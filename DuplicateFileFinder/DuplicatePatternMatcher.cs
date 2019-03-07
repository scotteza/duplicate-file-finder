using System.Collections.Generic;

namespace DuplicateFileFinder
{
    public interface DuplicatePatternMatcher
    {
        List<DuplicateFile> FindDuplicates(List<FileData> files);
    }
}