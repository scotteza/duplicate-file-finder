using DuplicateFileFinder.DuplicatePatternMatchers;
using DuplicateFileFinder.FileHashers;
using System;
using System.IO;
using System.Linq;
using DuplicateFileFinder.FileSizers;

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
            var windowsFileSizer = new WindowsFileSizer();
            var patternMatcher = new FileSizeDuplicatePatternMatcher(windowsFileSizer);
            var duplicateFiles = duplicateFileFinder.GetDuplicates(rootDirectory, patternMatcher);

            foreach (var duplicateFile in duplicateFiles)
            {
                Console.WriteLine($"Identified by: {duplicateFile.Identifier}. Total count: {duplicateFile.Count}.");
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
