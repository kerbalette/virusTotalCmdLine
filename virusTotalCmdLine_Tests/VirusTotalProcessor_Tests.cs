using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using virusTotalCmdline.Models;
using virusTotalCmdline.Processors;
using virusTotalCmdline.Utils;

namespace virusTotalCmdLine_Tests
{
    [TestClass]
    public class VirusTotalProcessor_Tests
    {
        [TestMethod]
        public async Task FileReport_Test()
        {
            ApiHelper.InitializeClient();
            UserApiMgmt userApiMgmt = new UserApiMgmt();

            var fileReport = NSubstitute.Substitute.For<VirusTotalProcessor>(userApiMgmt.ApiKey);
            FileReportResponse model = await fileReport.FileReport("00bb4a90c611483084cb9bc695635332a32fa3cabe4782b7f1251544a5a0607c");
            
            //FileReportResponseModel model = await fileReport.FileReport("00bb4a90c611483084cb9bc695635332a32fa3cabe4782b7f1251544a5a0607c");
            //Console.WriteLine("test");;
        }
    }
}
