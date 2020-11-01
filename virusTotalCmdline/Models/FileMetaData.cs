using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace virusTotalCmdline.Models
{
    public class FileMetaData
    {
        private string _md5Hash;
        private string _sha1Hash;
        private string _sha256Hash;
        private string _impHash;

        public FileMetaData(string ioc)
        {
            if (File.Exists(ioc))
            {
                FileName = Path.GetFileName(ioc);
                Folder = Path.GetDirectoryName(ioc);
            }
            else
            {
                if (ioc.Length == 20)
                    SHA1Hash = ioc;
                else if (ioc.Length == 64)
                    SHA256Hash = ioc;
                else
                    MD5Hash = ioc;
            }
        }

        public string FileName { get; set; }
        public string Folder { get; set; }

        public string MD5Hash
        {
            get
            {
                if (_md5Hash == null)
                    _md5Hash = CalculateMD5(Folder + "\\" + FileName);

                return _md5Hash;
            }
            private set { _md5Hash = value; }
        }
        public string SHA1Hash
        {
            get
            {
                if (_sha1Hash == null)
                    _sha1Hash = CalculateSHA1(Folder + "\\" + FileName);

                return _sha1Hash;
            }
            private set { _sha1Hash = value; }
        }
        public string SHA256Hash
        {
            get
            {
                if (_sha256Hash == null)
                    _sha256Hash = CalculateSHA256(Folder + "\\" + FileName);

                return _sha256Hash;
            }
            private set { _sha256Hash = value; }
        }

        public string ImpHash
        {
            get
            {
                if (_impHash == null)
                    _impHash = CalculateImpHash(Folder + "\\" + FileName);

                return _impHash;
            }
        }

        private string CalculateMD5(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        private string CalculateSHA1(string fileName)
        {
            using (var sha1 = SHA1.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(sha1.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        private string CalculateSHA256(string fileName)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(sha256.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        private string CalculateImpHash(string fileName)
        {
            var peHeader = new PeNet.PeFile(fileName);
            PeNet.ImportFunction[] functions = peHeader.ImportedFunctions;
            return peHeader.ImpHash;
        }
    }
}
