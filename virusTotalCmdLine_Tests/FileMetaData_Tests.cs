using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using virusTotalCmdline.Models;

namespace virusTotalCmdLine_Tests
{
    [TestClass]
    public class FileMetaData_Tests
    {
        [TestMethod]
        public void GetHash_checkValue()
        {
            // ARRANGE
            FileMetaData fileMetaData = new FileMetaData(@"C:\Windows\explorer.exe");

            // ACT
            string MD5Hash = fileMetaData.MD5Hash;
            string Sha1Hash = fileMetaData.SHA1Hash;
            string Sha256 = fileMetaData.SHA256Hash;
            string impHash = fileMetaData.ImpHash;

            // ASSERT
            Console.WriteLine(Sha256);
        }
        
    }
}
