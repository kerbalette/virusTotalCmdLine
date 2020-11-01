using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using virusTotalCmdline.Utils;

namespace virusTotalCmdLine_Tests
{
   [TestClass]
    public  class UserApiMgmt_Tests
    {
        // ability to specify APIKey
        // If there is an API then overwrite the token
        // If there is no API and no token file then error


        [TestMethod]
        public void tokenExists_notExist()
        {
            // ARRANGE
            UserApiMgmt userApiMgmt = new UserApiMgmt("supersecretapikey");

            // ACT
        

            // ASSERT
         
        }
    }
}
