using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace D2DataAccess.Extensions
{
    public static class WebExtensions
    {
        public async static Task<T> GetAsync<T>(this HttpClient Web, String Path,bool encode = true)
        {
            var result = await Web.GetAsync(encode ? HttpUtility.UrlEncode(Path) : Path).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                var body = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(body);
            }
            else
            {
                return default(T);
            }
        }
    }
}
