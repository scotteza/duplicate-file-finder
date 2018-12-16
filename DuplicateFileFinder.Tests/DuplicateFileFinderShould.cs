using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DuplicateFileFinder.Tests
{
    [TestFixture]
    internal class DuplicateFileFinderShould
    {
        private Mock<IDirectoryParser> _directoryParser;
        private DuplicateFileFinder _duplicateFileFinder;

        private string _rootDirectory;
        private List<DirectoryData> _directories;

        private string _duplicateFileName1;
        private string _duplicateFileName2;
        private List<FileData> _files;

        [SetUp]
        public void SetUp()
        {
            _rootDirectory = ".";
            _directories = new List<DirectoryData>
            {
                new DirectoryData("Directory 1", @"c:\Directory 1"),
                new DirectoryData("Directory 2", @"c:\Directory 2"),
                new DirectoryData("Directory 3", @"c:\Directory 3")
            };

            _duplicateFileName1 = "File 1.txt";
            _duplicateFileName2 = "File 2.txt";
            _files = new List<FileData>
            {
                new FileData(_duplicateFileName1),
                new FileData(_duplicateFileName1),
                new FileData(_duplicateFileName1),
                new FileData(_duplicateFileName2),
                new FileData(_duplicateFileName2),
                new FileData(_duplicateFileName2),
                new FileData(_duplicateFileName2),
                new FileData(_duplicateFileName2),
                new FileData(_duplicateFileName2),
                new FileData("File 3.txt")
            };

            _directoryParser = new Mock<IDirectoryParser>();
            _directoryParser
                .Setup(dp => dp.FindAllDirectories(_rootDirectory, IncludeRootDirectoryInResults.Yes))
                .Returns(_directories);
            _directoryParser
                .Setup(dp => dp.FindAllFiles(It.IsAny<List<DirectoryData>>()))
                .Returns(_files);

            _duplicateFileFinder = new DuplicateFileFinder(_directoryParser.Object);
        }

        [Test]
        public void Find_Duplicate_File_Using_A_DirectoryParser()
        {
            _duplicateFileFinder.GetDuplicates(_rootDirectory);

            _directoryParser.Verify(dp => dp.FindAllDirectories(_rootDirectory, IncludeRootDirectoryInResults.Yes));
            _directoryParser.Verify(dp => dp.FindAllFiles(_directories));
        }

        [Test]
        public void Find_Duplicate_File_By_Name()
        {
            var duplicates = _duplicateFileFinder.GetDuplicates(_rootDirectory);

            Assert.That(duplicates.Count, Is.EqualTo(2));
            Assert.That(duplicates.First(d => d.Name == _duplicateFileName1).Count, Is.EqualTo(3));
            Assert.That(duplicates.First(d => d.Name == _duplicateFileName2).Count, Is.EqualTo(6));
        }
    }
}
