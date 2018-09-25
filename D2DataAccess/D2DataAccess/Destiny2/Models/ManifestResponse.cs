using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Destiny2.Models
{
    public class ManifestResponse: BaseResponse
    {
        public Manifest Response { get; set; }
    }

    public class Manifest
    {
        public string version { get; set; }
        public string mobileAssestContentPath { get; set; }
        public Dictionary<string, string> mobileWorldContentPaths { get; set; }
        public string mobileClanBannerDatabasePath { get; set; }
        public Dictionary<string, string> mobileGearCDN { get; set; }
    }
}
