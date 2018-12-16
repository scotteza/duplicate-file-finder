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
            var directories = directoryParser.FindAllDirectories(rootDirectory, IncludeRootDirectoryInResults.Yes);
            var files = directoryParser.FindAllFiles(directories);

            Console.WriteLine(files.Count);
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
