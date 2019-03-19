using DuplicateFileFinder.DuplicatePatternMatchers;
using DuplicateFileFinder.FileHashers;
using DuplicateFileFinder.FileSizers;
using System;
using System.IO;
using System.Linq;

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
            var windowsFileSizer = new WindowsFileSizer();
            var patternMatcher = new FileHashDuplicatePatternMatcher(windowsFileHasher, windowsFileSizer);
            var duplicateFiles = duplicateFileFinder.GetDuplicates(rootDirectory, patternMatcher);

            foreach (var duplicateFile in duplicateFiles)
            {
                Console.WriteLine($"{duplicateFile.Identifier}: {duplicateFile.Count}");
                foreach (var distinctFilePath in duplicateFile.DistinctFilePaths)
                {
                    Console.WriteLine($"\t{distinctFilePath}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
        }

        private static void WriteInvalidDirectoryMessage()
        {
            Console.WriteLine("Please pass in a valid directory as an argument.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
