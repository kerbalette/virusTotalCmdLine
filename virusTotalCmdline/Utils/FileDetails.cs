using System;
using System.IO;
using System.Security.Cryptography;

namespace virusTotalCmdline.Utils
{
    class FileDetails
    {
        public static string GetSHA256HashFromFile(string filename)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(sha256.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }
    }
}
