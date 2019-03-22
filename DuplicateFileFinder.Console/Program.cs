using DuplicateFileFinder.DuplicatePatternMatchers;
using DuplicateFileFinder.FileHashers;
using DuplicateFileFinder.FileSizers;
using System;
using System.Collections.Generic;
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

            var duplicateFiles = GetDuplicateFiles(rootDirectory);

            WriteDuplicateFilesToConsole(duplicateFiles);

            WriteConsoleExitMessage();

            Console.ReadKey();
        }

        private static List<DuplicateFile> GetDuplicateFiles(string rootDirectory)
        {
            var directoryParser = new WindowsDirectoryParser();
            var duplicateFileFinder = new DuplicateFileFinder(directoryParser);

            var windowsFileSizer = new WindowsFileSizer();
            var fileSizeDuplicatePatternMatcher = new FileSizeDuplicatePatternMatcher(windowsFileSizer);

            var fileHasher = new WindowsFileHasher();
            var fileHashDuplicatePatternMatcher = new FileHashDuplicatePatternMatcher(fileHasher);

            var fileNameDuplicatePatternMatcher = new FileNameDuplicatePatternMatcher();

            var duplicateFiles = duplicateFileFinder.GetDuplicates(rootDirectory, fileHashDuplicatePatternMatcher);
            return duplicateFiles;
        }

        private static void WriteDuplicateFilesToConsole(List<DuplicateFile> duplicateFiles)
        {
            foreach (var duplicateFile in duplicateFiles)
            {
                Console.WriteLine($"Identified by: {duplicateFile.Identifier}. Total count: {duplicateFile.Count}.");
                foreach (var distinctFilePath in duplicateFile.DistinctFilePaths)
                {
                    Console.WriteLine($"\t{distinctFilePath}");
                }
            }
        }

        private static void WriteConsoleExitMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
        }

        private static void WriteInvalidDirectoryMessage()
        {
            Console.WriteLine("Please pass in a valid directory as an argument.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}
