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
            var duplicateFiles = duplicateFileFinder.GetDuplicates(rootDirectory);

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
