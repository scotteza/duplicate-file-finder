using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DuplicateFileFinder.Tests
{
    [TestFixture]
    internal class DuplicateFileFinderShould
    {
        [Test]
        public void Find_Duplicate_File_Using_A_DirectoryParser()
        {
            var rootDirectory = ".";
            var directoryData = new List<DirectoryData>
            {
                new DirectoryData("Directory 1", @"c:\Directory 1"),
                new DirectoryData("Directory 2", @"c:\Directory 2"),
                new DirectoryData("Directory 3", @"c:\Directory 3")
            };
            var directoryParser = new Mock<IDirectoryParser>();
            directoryParser.Setup(dp => dp.FindAllDirectories(rootDirectory, IncludeRootDirectoryInResults.Yes))
                .Returns(directoryData);
            var duplicateFileFinder = new DuplicateFileFinder(directoryParser.Object);

            var duplicates = duplicateFileFinder.GetDuplicates(rootDirectory);

            directoryParser.Verify(dp => dp.FindAllDirectories(rootDirectory, IncludeRootDirectoryInResults.Yes));
            directoryParser.Verify(dp => dp.FindAllFiles(directoryData));
        }
    }
}
