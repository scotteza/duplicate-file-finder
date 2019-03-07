using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DuplicateFileFinder.FileHashers
{
    public class WindowsFileHasher : FileHasher
    {
        public string HashFile(FileData fileData)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileData.FullName))
                {
                    var hash = md5.ComputeHash(stream);
                    return ByteHasToString(hash);
                }
            }
        }

        private string ByteHasToString(byte[] hash)
        {
            var resultBuilder = new StringBuilder();

            foreach (var b in hash)
            {
                resultBuilder.Append(b.ToString("x2"));
            }

            return resultBuilder.ToString();
        }
    }
}