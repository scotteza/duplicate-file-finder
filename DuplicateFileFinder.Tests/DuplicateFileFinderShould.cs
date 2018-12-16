using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DuplicateFileFinder.Tests
{
    [TestFixture]
    internal class DuplicateFileFinderShould
    {
        private Mock<IDirectoryParser> _directoryParser;
        private DuplicateFileFinder _duplicateFileFinder;
        private string _rootDirectory;

        [SetUp]
        public void SetUp()
        {
            _rootDirectory = ".";
            _directoryParser = new Mock<IDirectoryParser>();
            _duplicateFileFinder = new DuplicateFileFinder(_directoryParser.Object);
        }

        [Test]
        public void Find_Duplicate_File_Using_A_DirectoryParser()
        {
            var directoryData = new List<DirectoryData>
            {
                new DirectoryData("Directory 1", @"c:\Directory 1"),
                new DirectoryData("Directory 2", @"c:\Directory 2"),
                new DirectoryData("Directory 3", @"c:\Directory 3")
            };
            _directoryParser.Setup(dp => dp.FindAllDirectories(_rootDirectory, IncludeRootDirectoryInResults.Yes))
                .Returns(directoryData);
            
            var duplicates = _duplicateFileFinder.GetDuplicates(_rootDirectory);

            _directoryParser.Verify(dp => dp.FindAllDirectories(_rootDirectory, IncludeRootDirectoryInResults.Yes));
            _directoryParser.Verify(dp => dp.FindAllFiles(directoryData));
        }
    }
}
