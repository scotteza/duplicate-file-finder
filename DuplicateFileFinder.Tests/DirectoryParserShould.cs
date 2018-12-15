using System.IO;
using System.Linq;
using NUnit.Framework;

namespace DuplicateFileFinder.Tests
{
    [TestFixture]
    internal class DirectoryParserShould
    {
        private string _rootDirectory;
        private string _testFolder1Name;
        private string _testFolder2Name;
        private string _testFolder3Name;

        [SetUp]
        public void SetUp()
        {
            _rootDirectory = ".";
            _testFolder1Name = "Test 1";
            _testFolder2Name = "Test 2";
            _testFolder3Name = "Test 3";

            Directory.CreateDirectory(Path.Combine(_rootDirectory, _testFolder1Name));
            Directory.CreateDirectory(Path.Combine(_rootDirectory, _testFolder2Name));
            Directory.CreateDirectory(Path.Combine(_rootDirectory, _testFolder3Name));
        }

        [Test]
        public void Parse_A_Single_Level_Directory()
        {
            IDirectoryParser directoryParser = new WindowsDirectoryParser();

            var directories = directoryParser.FindAllDirectories(_rootDirectory);

            Assert.That(directories.Count, Is.EqualTo(3));
            Assert.That(directories.Any(d => d.Name == _testFolder1Name), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testFolder2Name), Is.EqualTo(true));
            Assert.That(directories.Any(d => d.Name == _testFolder3Name), Is.EqualTo(true));
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(Path.Combine(_rootDirectory, _testFolder1Name));
            Directory.Delete(Path.Combine(_rootDirectory, _testFolder2Name));
            Directory.Delete(Path.Combine(_rootDirectory, _testFolder3Name));
        }
    }
}
