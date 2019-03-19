using DuplicateFileFinder.DuplicatePatternMatchers;
using DuplicateFileFinder.FileSizers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DuplicateFileFinder.Tests
{
    public class FileSizeDuplicatePatternMatcherShould
    {
        private Mock<FileSizer> fileSizer;
        private FileSizeDuplicatePatternMatcher matcher;

        [SetUp]
        public void SetUp()
        {
            fileSizer = new Mock<FileSizer>();
            matcher = new FileSizeDuplicatePatternMatcher(fileSizer.Object);
        }

        [Test]
        public void Use_A_File_Sizer()
        {
            var file1 = new FileData("file 1.txt", fullName: null);
            var file2 = new FileData("file 2.txt", fullName: null);
            var file3 = new FileData("file 3.txt", fullName: null);
            var files = new List<FileData>
            {
                file1,
                file2,
                file3
            };

            matcher.FindDuplicates(files);

            fileSizer.Verify(x => x.SizeFile(file1));
            fileSizer.Verify(x => x.SizeFile(file2));
            fileSizer.Verify(x => x.SizeFile(file3));
        }

        [Test]
        public void Match_Files_With_The_Same_Size()
        {
            var file1 = new FileData("file 1.txt", fullName: null);
            var file2 = new FileData("file 2.txt", fullName: null);
            var file3 = new FileData("file 3.txt", fullName: null);
            var files = new List<FileData>
            {
                file1,
                file2,
                file3
            };
            fileSizer.Setup(x => x.SizeFile(file1)).Returns(42);
            fileSizer.Setup(x => x.SizeFile(file2)).Returns(42);
            fileSizer.Setup(x => x.SizeFile(file3)).Returns(42);

            var duplicates = matcher.FindDuplicates(files);

            Assert.That(duplicates.Count, Is.EqualTo(1));
            Assert.That(duplicates.Contains(new DuplicateFile(42, 3)));
        }

        [Test]
        public void Match_Multiple_Files_With_The_Same_Size()
        {
            var file1 = new FileData("file 1.txt", fullName: null);
            var file2 = new FileData("file 2.txt", fullName: null);
            var file3 = new FileData("file 3.txt", fullName: null);
            var file4 = new FileData("file 4.txt", fullName: null);
            var file5 = new FileData("file 5.txt", fullName: null);
            var file6 = new FileData("file 6.txt", fullName: null);
            var file7 = new FileData("file 7.txt", fullName: null);
            var file8 = new FileData("file 8.txt", fullName: null);
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
            fileSizer.Setup(x => x.SizeFile(file1)).Returns(42);
            fileSizer.Setup(x => x.SizeFile(file2)).Returns(42);
            fileSizer.Setup(x => x.SizeFile(file3)).Returns(42);
            fileSizer.Setup(x => x.SizeFile(file4)).Returns(12);
            fileSizer.Setup(x => x.SizeFile(file5)).Returns(12);
            fileSizer.Setup(x => x.SizeFile(file6)).Returns(1);
            fileSizer.Setup(x => x.SizeFile(file7)).Returns(2);
            fileSizer.Setup(x => x.SizeFile(file8)).Returns(3);

            var duplicates = matcher.FindDuplicates(files);

            Assert.That(duplicates.Count, Is.EqualTo(2));
            Assert.That(duplicates.Contains(new DuplicateFile(42, 3)));
            Assert.That(duplicates.Contains(new DuplicateFile(12, 2)));
        }

        [Test]
        public void Return_A_List_Of_Files_Per_Size()
        {
            var file1 = new FileData("file 1.txt", "Full name 1");
            var file2 = new FileData("file 2.txt", "Full name 2");
            var file3 = new FileData("file 3.txt", "Full name 3");
            var files = new List<FileData>
            {
                file1,
                file2,
                file3
            };
            fileSizer.Setup(x => x.SizeFile(file1)).Returns(42);
            fileSizer.Setup(x => x.SizeFile(file2)).Returns(42);
            fileSizer.Setup(x => x.SizeFile(file3)).Returns(42);

            var duplicates = matcher.FindDuplicates(files);

            Assert.That(duplicates.Count, Is.EqualTo(1));
            Assert.That(duplicates.First().DistinctFilePaths.Count, Is.EqualTo(3));
        }
    }
}
