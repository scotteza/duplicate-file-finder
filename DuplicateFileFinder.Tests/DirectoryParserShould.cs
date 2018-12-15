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

        private string _testDirectoryName1;
        private string _testDirectoryName2;
        private string _testDirectoryName3;

        private string _directoryPath1;
        private string _directoryPath2;
        private string _directoryPath3;

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
            SetDirectoryNames();
            SetFileNames();
            CreateDirectories();
            CreateFiles();
            _directoryParser = new WindowsDirectoryParser();
        }

        private void CreateFiles()
        {
            File.Create(_testFilePath1).Close();
            File.Create(_testFilePath2).Close();
            File.Create(_testFilePath3).Close();
        }

        private void CreateDirectories()
        {
            Directory.CreateDirectory(_directoryPath1);
            Directory.CreateDirectory(_directoryPath2);
            Directory.CreateDirectory(_directoryPath3);
        }

        private void SetFileNames()
        {
            _testFileName1 = "Test File 1.txt";
            _testFileName2 = "Test File 2.txt";
            _testFileName3 = "Test File 3.txt";

            _testFilePath1 = Path.Combine(_directoryPath1, _testFileName1);
            _testFilePath2 = Path.Combine(_directoryPath1, _testFileName2);
            _testFilePath3 = Path.Combine(_directoryPath1, _testFileName3);
        }

        private void SetDirectoryNames()
        {
            _rootDirectory = @".\TestFolder";

            _testDirectoryName1 = "Test Folder 1";
            _testDirectoryName2 = "Test Folder 2";
            _testDirectoryName3 = "Test Folder 3";

            _directoryPath1 = Path.Combine(_rootDirectory, _testDirectoryName1);
            _directoryPath2 = Path.Combine(_rootDirectory, _testDirectoryName2);
            _directoryPath3 = Path.Combine(_rootDirectory, _testDirectoryName3);
        }

        [Test]
        public void Parse_A_Single_Level_Directory()
        {
            var directories = _directoryParser.FindAllDirectories(_rootDirectory);

            Assert.That(directories.Count, Is.EqualTo(3));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName1), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName2), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName3), Is.EqualTo(true));
        }

        [Test]
        public void Find_Files_In_A_Single_Directory()
        {
            var directories = _directoryParser.FindAllDirectories(_rootDirectory);
            var directoryData = directories.First(d => d.Name == _testDirectoryName1);
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

            Directory.Delete(_directoryPath1, recursive: false);
            Directory.Delete(_directoryPath2, recursive: false);
            Directory.Delete(_directoryPath3, recursive: false);
        }
    }
}
