using NUnit.Framework;
using System.IO;
using System.Linq;

namespace DuplicateFileFinder.Tests
{
    [TestFixture]
    internal class DirectoryParserShould
    {
        private string _rootDirectoryName;
        private string _testDirectoryName1;
        private string _testDirectoryName2;
        private string _testDirectoryName3;
        private string _testDirectoryName4;
        private string _testDirectoryName5;
        private string _testDirectoryName6;

        private string _rootDirectoryPath;
        private string _directoryPath1;
        private string _directoryPath2;
        private string _directoryPath3;
        private string _directoryPath4;
        private string _directoryPath5;
        private string _directoryPath6;

        private string _testFileName1;
        private string _testFileName2;
        private string _testFileName3;
        private string _testFileName4;
        private string _testFileName5;
        private string _testFileName6;
        private string _testFileName7;

        private string _testFilePath1;
        private string _testFilePath2;
        private string _testFilePath3;
        private string _testFilePath4;
        private string _testFilePath5;
        private string _testFilePath6;
        private string _testFilePath7;

        private IDirectoryParser _directoryParser;

        [SetUp]
        public void SetUp()
        {
            SetDirectoryNames();
            SetFileNames();
            CreateDirectories();
            CreateFiles();
            CreateDirectoryParser();
        }

        private void SetDirectoryNames()
        {
            _rootDirectoryName = "Root Test Folder";
            _testDirectoryName1 = "Test Folder 1";
            _testDirectoryName2 = "Test Folder 2";
            _testDirectoryName3 = "Test Folder 3";
            _testDirectoryName4 = "Test Folder 4";
            _testDirectoryName5 = "Test Folder 5";
            _testDirectoryName6 = "Test Folder 6";

            _rootDirectoryPath = Path.Combine(".", _rootDirectoryName);
            _directoryPath1 = Path.Combine(_rootDirectoryPath, _testDirectoryName1);
            _directoryPath2 = Path.Combine(_rootDirectoryPath, _testDirectoryName2);
            _directoryPath3 = Path.Combine(_rootDirectoryPath, _testDirectoryName3);
            // Deeper-level directories
            _directoryPath4 = Path.Combine(_directoryPath1, _testDirectoryName4);
            _directoryPath5 = Path.Combine(_directoryPath4, _testDirectoryName5);
            _directoryPath6 = Path.Combine(_directoryPath4, _testDirectoryName6);
        }

        private void SetFileNames()
        {
            _testFileName1 = "Test File 1.txt";
            _testFileName2 = "Test File 2.txt";
            _testFileName3 = "Test File 3.txt";
            _testFileName4 = "Test File 4.txt";
            _testFileName5 = "Test File 5.txt";
            _testFileName6 = "Test File 6.txt";
            _testFileName7 = "Test File 7.txt";

            _testFilePath1 = Path.Combine(_directoryPath1, _testFileName1);
            _testFilePath2 = Path.Combine(_directoryPath1, _testFileName2);
            _testFilePath3 = Path.Combine(_directoryPath1, _testFileName3);
            _testFilePath4 = Path.Combine(_directoryPath4, _testFileName4);
            _testFilePath5 = Path.Combine(_directoryPath6, _testFileName5);
            _testFilePath6 = Path.Combine(_directoryPath6, _testFileName6);
            _testFilePath7 = Path.Combine(_rootDirectoryPath, _testFileName7);
        }

        private void CreateDirectories()
        {
            Directory.CreateDirectory(_rootDirectoryPath);
            Directory.CreateDirectory(_directoryPath1);
            Directory.CreateDirectory(_directoryPath2);
            Directory.CreateDirectory(_directoryPath3);
            Directory.CreateDirectory(_directoryPath4);
            Directory.CreateDirectory(_directoryPath5);
            Directory.CreateDirectory(_directoryPath6);
        }

        private void CreateFiles()
        {
            File.Create(_testFilePath1).Close();
            File.Create(_testFilePath2).Close();
            File.Create(_testFilePath3).Close();
            File.Create(_testFilePath4).Close();
            File.Create(_testFilePath5).Close();
            File.Create(_testFilePath6).Close();
            File.Create(_testFilePath7).Close();
        }

        private void CreateDirectoryParser()
        {
            _directoryParser = new WindowsDirectoryParser();
        }

        [Test]
        public void Parse_A_Directory_Structure()
        {
            var directories = _directoryParser.FindAllDirectories(_rootDirectoryPath, IncludeRootDirectoryInResults.No);

            Assert.That(directories.Count, Is.EqualTo(6));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName1), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName2), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName3), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName4), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName5), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName6), Is.EqualTo(true));
        }

        [Test]
        public void Parse_A_Directory_Structure_And_Include_The_Root_Directory_In_Results()
        {
            var directories = _directoryParser.FindAllDirectories(_rootDirectoryPath, IncludeRootDirectoryInResults.Yes);

            Assert.That(directories.Count, Is.EqualTo(7));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName1), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName2), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName3), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName4), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName5), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testDirectoryName6), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _rootDirectoryName), Is.EqualTo(true));
        }

        [Test]
        public void Find_Files_In_A_Single_Directory()
        {
            var directories = _directoryParser.FindAllDirectories(_rootDirectoryPath, IncludeRootDirectoryInResults.Yes);
            var directoryData = directories.First(d => d.Name == _testDirectoryName1);
            var files = _directoryParser.FindAllFiles(directoryData);

            Assert.That(files.Count, Is.EqualTo(3));
            Assert.That(files.Any(f => f.Name == _testFileName1), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == _testFileName2), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == _testFileName3), Is.EqualTo(true));
        }

        [Test]
        public void Find_Files_In_A_Collection_Of_Directories()
        {
            var directories = _directoryParser.FindAllDirectories(_rootDirectoryPath, IncludeRootDirectoryInResults.Yes);
            var files = _directoryParser.FindAllFiles(directories);

            Assert.That(files.Count, Is.EqualTo(7));
            Assert.That(files.Any(f => f.Name == _testFileName1), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == _testFileName2), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == _testFileName3), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == _testFileName4), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == _testFileName5), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == _testFileName6), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == _testFileName7), Is.EqualTo(true));
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_testFilePath1);
            File.Delete(_testFilePath2);
            File.Delete(_testFilePath3);
            File.Delete(_testFilePath4);
            File.Delete(_testFilePath5);
            File.Delete(_testFilePath6);
            File.Delete(_testFilePath7);

            Directory.Delete(_directoryPath5, recursive: false);
            Directory.Delete(_directoryPath6, recursive: false);
            Directory.Delete(_directoryPath4, recursive: false);
            Directory.Delete(_directoryPath2, recursive: false);
            Directory.Delete(_directoryPath3, recursive: false);
            Directory.Delete(_directoryPath1, recursive: false);
            Directory.Delete(_rootDirectoryPath, recursive: false);
        }
    }
}
