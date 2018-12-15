using Moq;
using NUnit.Framework;

namespace DuplicateFileFinder.Tests
{
    [TestFixture]
    internal class FileFinderShould
    {
        [Test]
        public void Read_Files_From_Directory_Parser()
        {
            var rootDirectory = "root";
            var directoryParser = new Mock<IDirectoryParser>();

            var fileFinder = new FileFinder(directoryParser.Object);
            fileFinder.FindAllFiles(rootDirectory);

            directoryParser.Verify(dp => dp.FindAllDirectories(rootDirectory));
        }
    }
}
