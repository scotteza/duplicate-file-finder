using NUnit.Framework;
using System.IO;
using System.Linq;

namespace DuplicateFileFinder.Tests
{
    [TestFixture]
    internal class WindowsDirectoryParserShould
    {
        private string rootDirectoryName;
        private string testDirectoryName1;
        private string testDirectoryName2;
        private string testDirectoryName3;
        private string testDirectoryName4;
        private string testDirectoryName5;
        private string testDirectoryName6;

        private string rootDirectoryPath;
        private string directoryPath1;
        private string directoryPath2;
        private string directoryPath3;
        private string directoryPath4;
        private string directoryPath5;
        private string directoryPath6;

        private string testFileName1;
        private string testFileName2;
        private string testFileName3;
        private string testFileName4;
        private string testFileName5;
        private string testFileName6;
        private string testFileName7;

        private string testFilePath1;
        private string testFilePath2;
        private string testFilePath3;
        private string testFilePath4;
        private string testFilePath5;
        private string testFilePath6;
        private string testFilePath7;

        private WindowsDirectoryParser directoryParser;

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
            rootDirectoryName = "Root Test Folder";
            testDirectoryName1 = "Test Folder 1";
            testDirectoryName2 = "Test Folder 2";
            testDirectoryName3 = "Test Folder 3";
            testDirectoryName4 = "Test Folder 4";
            testDirectoryName5 = "Test Folder 5";
            testDirectoryName6 = "Test Folder 6";

            rootDirectoryPath = Path.Combine(".", rootDirectoryName);
            directoryPath1 = Path.Combine(rootDirectoryPath, testDirectoryName1);
            directoryPath2 = Path.Combine(rootDirectoryPath, testDirectoryName2);
            directoryPath3 = Path.Combine(rootDirectoryPath, testDirectoryName3);
            // Deeper-level directories
            directoryPath4 = Path.Combine(directoryPath1, testDirectoryName4);
            directoryPath5 = Path.Combine(directoryPath4, testDirectoryName5);
            directoryPath6 = Path.Combine(directoryPath4, testDirectoryName6);
        }

        private void SetFileNames()
        {
            testFileName1 = "Test File 1.txt";
            testFileName2 = "Test File 2.txt";
            testFileName3 = "Test File 3.txt";
            testFileName4 = "Test File 4.txt";
            testFileName5 = "Test File 5.txt";
            testFileName6 = "Test File 6.txt";
            testFileName7 = "Test File 7.txt";

            testFilePath1 = Path.Combine(directoryPath1, testFileName1);
            testFilePath2 = Path.Combine(directoryPath1, testFileName2);
            testFilePath3 = Path.Combine(directoryPath1, testFileName3);
            testFilePath4 = Path.Combine(directoryPath4, testFileName4);
            testFilePath5 = Path.Combine(directoryPath6, testFileName5);
            testFilePath6 = Path.Combine(directoryPath6, testFileName6);
            testFilePath7 = Path.Combine(rootDirectoryPath, testFileName7);
        }

        private void CreateDirectories()
        {
            Directory.CreateDirectory(rootDirectoryPath);
            Directory.CreateDirectory(directoryPath1);
            Directory.CreateDirectory(directoryPath2);
            Directory.CreateDirectory(directoryPath3);
            Directory.CreateDirectory(directoryPath4);
            Directory.CreateDirectory(directoryPath5);
            Directory.CreateDirectory(directoryPath6);
        }

        private void CreateFiles()
        {
            File.Create(testFilePath1).Close();
            File.Create(testFilePath2).Close();
            File.Create(testFilePath3).Close();
            File.Create(testFilePath4).Close();
            File.Create(testFilePath5).Close();
            File.Create(testFilePath6).Close();
            File.Create(testFilePath7).Close();
        }

        private void CreateDirectoryParser()
        {
            directoryParser = new WindowsDirectoryParser();
        }

        [Test]
        public void Parse_A_Directory_Structure()
        {
            var directories = directoryParser.FindAllDirectories(rootDirectoryPath, IncludeRootDirectoryInResults.No);

            Assert.That(directories.Count, Is.EqualTo(6));
            Assert.That(directories.Any(d => d.Name == testDirectoryName1), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == testDirectoryName2), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == testDirectoryName3), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == testDirectoryName4), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == testDirectoryName5), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == testDirectoryName6), Is.EqualTo(true));
        }

        [Test]
        public void Parse_A_Directory_Structure_And_Include_The_Root_Directory_In_Results()
        {
            var directories = directoryParser.FindAllDirectories(rootDirectoryPath, IncludeRootDirectoryInResults.Yes);

            Assert.That(directories.Count, Is.EqualTo(7));
            Assert.That(directories.Any(d => d.Name == testDirectoryName1), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == testDirectoryName2), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == testDirectoryName3), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == testDirectoryName4), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == testDirectoryName5), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == testDirectoryName6), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == rootDirectoryName), Is.EqualTo(true));
        }

        [Test]
        public void Find_Files_In_A_Single_Directory()
        {
            var directories = directoryParser.FindAllDirectories(rootDirectoryPath, IncludeRootDirectoryInResults.Yes);
            var directoryData = directories.First(d => d.Name == testDirectoryName1);
            var files = directoryParser.FindAllFiles(directoryData);

            Assert.That(files.Count, Is.EqualTo(3));
            Assert.That(files.Any(f => f.Name == testFileName1), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == testFileName2), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == testFileName3), Is.EqualTo(true));
        }

        [Test]
        public void Find_Files_In_A_Collection_Of_Directories()
        {
            var directories = directoryParser.FindAllDirectories(rootDirectoryPath, IncludeRootDirectoryInResults.Yes);
            var files = directoryParser.FindAllFiles(directories);

            var currentDirectory = Directory.GetCurrentDirectory();

            Assert.That(files.Count, Is.EqualTo(7));
            Assert.That(files.Any(f => f.Name == testFileName1 && f.FullName == Path.GetFullPath(testFilePath1)), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == testFileName2 && f.FullName == Path.GetFullPath(testFilePath2)), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == testFileName3 && f.FullName == Path.GetFullPath(testFilePath3)), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == testFileName4 && f.FullName == Path.GetFullPath(testFilePath4)), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == testFileName5 && f.FullName == Path.GetFullPath(testFilePath5)), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == testFileName6 && f.FullName == Path.GetFullPath(testFilePath6)), Is.EqualTo(true));
            Assert.That(files.Any(f => f.Name == testFileName7 && f.FullName == Path.GetFullPath(testFilePath7)), Is.EqualTo(true));
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(testFilePath1);
            File.Delete(testFilePath2);
            File.Delete(testFilePath3);
            File.Delete(testFilePath4);
            File.Delete(testFilePath5);
            File.Delete(testFilePath6);
            File.Delete(testFilePath7);

            Directory.Delete(directoryPath5, recursive: false);
            Directory.Delete(directoryPath6, recursive: false);
            Directory.Delete(directoryPath4, recursive: false);
            Directory.Delete(directoryPath2, recursive: false);
            Directory.Delete(directoryPath3, recursive: false);
            Directory.Delete(directoryPath1, recursive: false);
            Directory.Delete(rootDirectoryPath, recursive: false);
        }
    }
}
