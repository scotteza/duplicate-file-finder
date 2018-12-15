using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace DuplicateFileFinder.Tests
{
    [TestFixture]
    internal class DirectoryParserShould
    {
        private string _rootDirectory;

        private string _testFolderName1;
        private string _testFolderName2;
        private string _testFolderName3;

        private string _folderPath1;
        private string _folderPath2;
        private string _folderPath3;

        private string _testFileName1;
        private string _testFileName2;
        private string _testFileName3;

        private string _testFilePath1;
        private string _testFilePath2;
        private string _testFilePath3;

        private IDirectoryParser _directoryParser;

        [SetUp]
        public void SetUp()
        {
            _rootDirectory = @".\TestFolder";

            _testFolderName1 = "Test Folder 1";
            _testFolderName2 = "Test Folder 2";
            _testFolderName3 = "Test Folder 3";

            _folderPath1 = Path.Combine(_rootDirectory, _testFolderName1);
            _folderPath2 = Path.Combine(_rootDirectory, _testFolderName2);
            _folderPath3 = Path.Combine(_rootDirectory, _testFolderName3);

            _testFileName1 = "Test File 1.txt";
            _testFileName2 = "Test File 2.txt";
            _testFileName3 = "Test File 3.txt";

            _testFilePath1 = Path.Combine(_folderPath1, _testFileName1);
            _testFilePath2 = Path.Combine(_folderPath1, _testFileName2);
            _testFilePath3 = Path.Combine(_folderPath1, _testFileName3);

            Directory.CreateDirectory(_folderPath1);
            Directory.CreateDirectory(_folderPath2);
            Directory.CreateDirectory(_folderPath3);

            File.Create(_testFilePath1).Close();
            File.Create(_testFilePath2).Close();
            File.Create(_testFilePath3).Close();

            _directoryParser = new WindowsDirectoryParser();
        }

        [Test]
        public void Parse_A_Single_Level_Directory()
        {
            var directories = _directoryParser.FindAllDirectories(_rootDirectory);

            Assert.That(directories.Count, Is.EqualTo(3));
            Assert.That(directories.Any(d => d.Name == _testFolderName1), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testFolderName2), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testFolderName3), Is.EqualTo(true));
        }

        [Test]
        public void Find_Files_In_A_Single_Directory()
        {
            var directories = _directoryParser.FindAllDirectories(_rootDirectory);
            var directoryData = directories.First(d => d.Name == _testFolderName1);
            var files = _directoryParser.FindAllFiles(directoryData);

            Assert.That(files.Count, Is.EqualTo(3));
            Assert.That(files.Any(f => f.Name == _testFileName1), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == _testFileName2), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == _testFileName3), Is.EqualTo(true));
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_testFilePath1);
            File.Delete(_testFilePath2);
            File.Delete(_testFilePath3);

            Directory.Delete(_folderPath1, recursive: false);
            Directory.Delete(_folderPath2, recursive: false);
            Directory.Delete(_folderPath3, recursive: false);
        }
    }
}
