using DuplicateFileFinder.DuplicatePatternMatchers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DuplicateFileFinder.Tests
{
    [TestFixture]
    internal class DuplicateFileFinderShould
    {
        private Mock<IDirectoryParser> directoryParser;
        private DuplicateFileFinder duplicateFileFinder;
        private FileNameDuplicatePatternMatcher fileNameDuplicatePatternMatcher;

        private string rootDirectory;
        private List<DirectoryData> directories;

        private string duplicateFileName1;
        private string duplicateFileName2;
        private List<FileData> files;

        [SetUp]
        public void SetUp()
        {
            fileNameDuplicatePatternMatcher = new FileNameDuplicatePatternMatcher();

            rootDirectory = ".";
            directories = new List<DirectoryData>
            {
                new DirectoryData("Directory 1", @"c:\Directory 1"),
                new DirectoryData("Directory 2", @"c:\Directory 2"),
                new DirectoryData("Directory 3", @"c:\Directory 3")
            };

            duplicateFileName1 = "File 1.txt";
            duplicateFileName2 = "File 2.txt";
            files = new List<FileData>
            {
                new FileData(duplicateFileName1, fullName: null),
                new FileData(duplicateFileName1, fullName: null),
                new FileData(duplicateFileName1, fullName: null),
                new FileData(duplicateFileName2, fullName: null),
                new FileData(duplicateFileName2, fullName: null),
                new FileData(duplicateFileName2, fullName: null),
                new FileData(duplicateFileName2, fullName: null),
                new FileData(duplicateFileName2, fullName: null),
                new FileData(duplicateFileName2, fullName: null),
                new FileData("File 3.txt", fullName: null)
            };

            directoryParser = new Mock<IDirectoryParser>();
            directoryParser
                .Setup(dp => dp.FindAllDirectories(rootDirectory, IncludeRootDirectoryInResults.Yes))
                .Returns(directories);
            directoryParser
                .Setup(dp => dp.FindAllFiles(It.IsAny<List<DirectoryData>>()))
                .Returns(files);

            duplicateFileFinder = new DuplicateFileFinder(directoryParser.Object);
        }

        [Test]
        public void Find_Duplicate_File_Using_A_DirectoryParser()
        {
            duplicateFileFinder.GetDuplicates(rootDirectory, fileNameDuplicatePatternMatcher);

            directoryParser.Verify(dp => dp.FindAllDirectories(rootDirectory, IncludeRootDirectoryInResults.Yes));
            directoryParser.Verify(dp => dp.FindAllFiles(directories));
        }

        [Test]
        public void Find_Duplicate_File_By_Name()
        {
            var duplicates = duplicateFileFinder.GetDuplicates(rootDirectory, fileNameDuplicatePatternMatcher);

            Assert.That(duplicates.Count, Is.EqualTo(2));
            Assert.That(duplicates.First(d => d.Identifier == duplicateFileName1).Count, Is.EqualTo(3));
            Assert.That(duplicates.First(d => d.Identifier == duplicateFileName2).Count, Is.EqualTo(6));
        }
    }
}
