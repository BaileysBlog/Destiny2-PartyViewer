using D2DataAccess.Models;
using D2DataAccess.SqLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using D2DataAccess.Extensions;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace D2DataAccess.Data
{
    public partial class Destiny2Api
    {
        public String ApiKey { get; private set; }

        public UserAgentHeader UserAgent { get; private set; }

        public SQLiteDestinyEngine DataEngine;

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
            _Web.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", UserAgent.ToString());
            _Web.DefaultRequestHeaders.Add("Accept", "applicaiton/json");

            InitDataWithoutPath();
        }

        public Destiny2Api(String Key, UserAgentHeader Header, String DB_Path)
        {
            ApiKey = Key;
            UserAgent = Header;
            _Web.BaseAddress = new Uri(ApiRootPath);
            _Web.DefaultRequestHeaders.Add("X-API-Key", ApiKey);
            _Web.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", UserAgent.ToString());
            _Web.DefaultRequestHeaders.Add("Accept", "applicaiton/json");

            DataEngine = new SQLiteDestinyEngine(new FileInfo(DB_Path));
        }

        public Destiny2Api(String Key, UserAgentHeader Header, DirectoryInfo DB_Root)
        {
            ApiKey = Key;
            UserAgent = Header;
            _Web.BaseAddress = new Uri(ApiRootPath);
            _Web.DefaultRequestHeaders.Add("X-API-Key", ApiKey);
            _Web.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", UserAgent.ToString());
            _Web.DefaultRequestHeaders.Add("Accept", "applicaiton/json");

            InitDataWithoutPath();
        }

        public Destiny2Api(String Key, UserAgentHeader Header)
        {
            ApiKey = Key;
            UserAgent = Header;
            _Web.BaseAddress = new Uri(ApiRootPath);
            _Web.DefaultRequestHeaders.Add("X-API-Key", ApiKey);
            _Web.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", UserAgent.ToString());
            _Web.DefaultRequestHeaders.Add("Accept", "applicaiton/json");

            InitDataWithoutPath();
        }

        private void InitDataWithoutPath()
        {
            var alreadyHasFile = new DirectoryInfo(Environment.CurrentDirectory).GetFiles("*.content", SearchOption.AllDirectories).FirstOrDefault();
            DataEngine = new SQLiteDestinyEngine(new FileInfo(Path.Combine(Environment.CurrentDirectory, alreadyHasFile != null ? alreadyHasFile.FullName : "FakeDB.content")));
        }

        public String GetBungieLink(String IconPath)
        {
            return String.Concat("https://www.bungie.net", IconPath);
        }

        public async Task<Nullable<bool>> DatabaseNeedsUpdate(String locale = "en")
        {
            // Load the manifest and check the name of the current db file against the returned value
            var curDb = DataEngine.CurrentDatabase;

            var manfiest = await GetDestinyManifest();

            if (manfiest.Response != null && manfiest.Response.mobileWorldContentPaths.ContainsKey(locale))
            {
                var db = manfiest.Response.mobileWorldContentPaths.Where(x => x.Key == locale).Select(x => x.Value).First();
                var fileName = db.Split('/').Last();
                if (curDb.Exists && Path.GetFileName(curDb.FullName) == fileName)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
