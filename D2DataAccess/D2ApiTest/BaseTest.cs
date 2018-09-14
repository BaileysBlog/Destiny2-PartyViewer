using D2DataAccess.Data;
using D2DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2ApiTest
{
    [TestClass]
    public class BaseTest
    {
        protected const string ApiKey = "57a4ba76e897450f8b106635fd20460b"; // I don't give a heck this can leak if it needs to I will disable it when I am not using it anyway.
        public Destiny2Api Api;

        public BaseTest()
        {
            Api = new Destiny2Api(ApiKey, new UserAgentHeader("Destiny 2 Party Viewer", "1.0.0", "Unit Tests", 0, "https://www.d2-partyviewer.com", "baileymiller@live.com"));
        }
    }
}
