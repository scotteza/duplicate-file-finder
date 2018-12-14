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
            var rootDirectory = "root";
            var directoryParser = new Mock<IDirectoryParser>();

            var duplicateFinder = new DuplicateFinder(directoryParser.Object);
            duplicateFinder.FindAllFiles(rootDirectory);

            directoryParser.Verify(dp => dp.FindAllFiles(rootDirectory));
        }
    }
}
