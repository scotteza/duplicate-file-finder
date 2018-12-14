using Moq;
using NUnit.Framework;

namespace DuplicateFileFinder.Tests
{
    [TestFixture]
    class DuplicateFileFinderShould
    {
        [Test]
        public void Read_Files_From_Directory_Parser()
        {
            var directoryParser = new Mock<IDirectoryParser>();
            var rootDirectory = "root";

            var duplicateFinder = new DuplicateFinder(directoryParser.Object);
            var result = duplicateFinder.FindAllFiles();

            directoryParser.Verify(dp => dp.FindAllFiles(rootDirectory));
        }
    }
}
