using System.Collections.Generic;

namespace DuplicateFileFinder.DuplicatePatternMatchers
{
    public interface DuplicatePatternMatcher
    {
        List<DuplicateFile> FindDuplicates(List<FileData> files);
    }
}