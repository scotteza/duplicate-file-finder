using DuplicateFileFinder.DuplicatePatternMatchers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DuplicateFileFinder.Tests
{
    public class FileHashDuplicatePatternMatcherShould
    {
        private Mock<FileHasher> _fileHasher;
        private FileHashDuplicatePatternMatcher _matcher;

        [SetUp]
        public void SetUp()
        {
            _fileHasher = new Mock<FileHasher>();
            _matcher = new FileHashDuplicatePatternMatcher(_fileHasher.Object);
        }

        [Test]
        public void Use_A_File_Hasher()
        {
            var file1 = new FileData("file 1.txt");
            var file2 = new FileData("file 2.txt");
            var file3 = new FileData("file 3.txt");
            var files = new List<FileData>
            {
                file1,
                file2,
                file3
            };

            _matcher.FindDuplicates(files);

            _fileHasher.Verify(x => x.HashFile(file1));
            _fileHasher.Verify(x => x.HashFile(file2));
            _fileHasher.Verify(x => x.HashFile(file3));
        }

        [Test]
        public void Match_Files_With_The_Same_Hash()
        {
            var file1 = new FileData("file 1.txt");
            var file2 = new FileData("file 2.txt");
            var file3 = new FileData("file 3.txt");
            var files = new List<FileData>
            {
                file1,
                file2,
                file3
            };
            _fileHasher.Setup(x => x.HashFile(file1)).Returns("ABC");
            _fileHasher.Setup(x => x.HashFile(file2)).Returns("ABC");
            _fileHasher.Setup(x => x.HashFile(file3)).Returns("ABC");

            var duplicates = _matcher.FindDuplicates(files);

            Assert.That(duplicates.Count, Is.EqualTo(1));
            Assert.That(duplicates.Contains(new DuplicateFile("ABC", 3)));
        }

        [Test]
        public void Match_Multiple_Files_With_The_Same_Hash()
        {
            var file1 = new FileData("file 1.txt");
            var file2 = new FileData("file 2.txt");
            var file3 = new FileData("file 3.txt");
            var file4 = new FileData("file 4.txt");
            var file5 = new FileData("file 5.txt");
            var file6 = new FileData("file 6.txt");
            var file7 = new FileData("file 7.txt");
            var file8 = new FileData("file 8.txt");
            var files = new List<FileData>
            {
                file1,
                file2,
                file3,
                file4,
                file5,
                file6,
                file7,
                file8
            };
            _fileHasher.Setup(x => x.HashFile(file1)).Returns("ABC");
            _fileHasher.Setup(x => x.HashFile(file2)).Returns("ABC");
            _fileHasher.Setup(x => x.HashFile(file3)).Returns("ABC");
            _fileHasher.Setup(x => x.HashFile(file4)).Returns("DEF");
            _fileHasher.Setup(x => x.HashFile(file5)).Returns("DEF");
            _fileHasher.Setup(x => x.HashFile(file6)).Returns("XXX");
            _fileHasher.Setup(x => x.HashFile(file7)).Returns("YYY");
            _fileHasher.Setup(x => x.HashFile(file8)).Returns("ZZZ");

            var duplicates = _matcher.FindDuplicates(files);

            Assert.That(duplicates.Count, Is.EqualTo(2));
            Assert.That(duplicates.Contains(new DuplicateFile("ABC", 3)));
            Assert.That(duplicates.Contains(new DuplicateFile("DEF", 2)));
        }
    }
}
