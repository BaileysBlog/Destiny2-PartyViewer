using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace D2DataAccess.Data
{
    public partial class Destiny2Api
    {
        public String ApiKey { get; private set; }

        public UserAgentHeader UserAgent { get; private set; }

        private HttpClient _Web { get; set; } = new HttpClient(new HttpClientHandler()
        {
            AllowAutoRedirect = true,
            MaxAutomaticRedirections = 100
        });

        public const String ApiRootPath = "https://www.bungie.net/Platform/";

        public Destiny2Api(String Key)
        {
            ApiKey = Key;
            _Web.BaseAddress = new Uri(ApiRootPath);
            _Web.DefaultRequestHeaders.Add("X-API-Key", ApiKey);
            _Web.DefaultRequestHeaders.Add("Accept", "applicaiton/json");
        }

        public Destiny2Api(String Key, UserAgentHeader Header)
        {
            ApiKey = Key;
            UserAgent = Header;
            _Web.BaseAddress = new Uri(ApiRootPath);
            _Web.DefaultRequestHeaders.Add("X-API-Key", ApiKey);
            _Web.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", UserAgent.ToString());
            _Web.DefaultRequestHeaders.Add("Accept", "applicaiton/json");
        }
    }
}
