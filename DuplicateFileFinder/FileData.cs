﻿namespace DuplicateFileFinder
{
    public class FileData
    {
        public string Name { get; }
        public string FullName { get; }

        public FileData(string name, string fullName = null)
        {
            Name = name;
            FullName = fullName;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
