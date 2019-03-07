using DuplicateFileFinder.DuplicatePatternMatchers;
using System;
using System.IO;
using System.Linq;
using DuplicateFileFinder.FileHashers;

namespace DuplicateFileFinder.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                WriteInvalidDirectoryMessage();
                return;
            }

            var rootDirectory = args.First();

            try
            {
                if (!Directory.Exists(rootDirectory))
                {
                    WriteInvalidDirectoryMessage();
                    return;
                }
            }
            catch
            {
                WriteInvalidDirectoryMessage();
                return;
            }

            var directoryParser = new WindowsDirectoryParser();
            var duplicateFileFinder = new DuplicateFileFinder(directoryParser);
            var windowsFileHasher = new WindowsFileHasher();
            var patternMatcher = new FileHashDuplicatePatternMatcher(windowsFileHasher);
            var duplicateFiles = duplicateFileFinder.GetDuplicates(rootDirectory, patternMatcher);

            foreach (var duplicateFile in duplicateFiles)
            {
                Console.WriteLine($"{duplicateFile.Name}: {duplicateFile.Count}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static void WriteInvalidDirectoryMessage()
        {
            Console.WriteLine("Please pass in a valid directory as an argument.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
