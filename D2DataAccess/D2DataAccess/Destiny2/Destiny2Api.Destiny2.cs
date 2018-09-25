using D2DataAccess.Destiny2.Models;
using D2DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using D2DataAccess.Extensions;
using System.Linq;
using System.IO;

namespace D2DataAccess.Data
{
    public partial class Destiny2Api
    {
        public async Task<SearchDestinyPlayerResponse> SearchDestinyPlayer(String displayName, BungieMembershipType membershipType)
        {
            string path = $"Destiny2/SearchDestinyPlayer/{(int)membershipType}/{displayName}/";
            return await _Web.GetAsync<SearchDestinyPlayerResponse>(path).ConfigureAwait(false);
        }

        public async Task<DestinyProfileResponse> GetProfile(long destinyMembershipId, BungieMembershipType membershipType, params DestinyComponentType[] components)
        {
            string path = $"Destiny2/{(int)membershipType}/Profile/{destinyMembershipId}/?components={FormatComponents(components)}";
            return await _Web.GetAsync<DestinyProfileResponse>(path, false).ConfigureAwait(false);
        }


        public async Task<ManifestResponse> GetDestinyManifest()
        {
            string path = "Destiny2/Manifest/";
            return await _Web.GetAsync<ManifestResponse>(path).ConfigureAwait(false);
        }

        public async Task<bool> UpdateDatabaseIfRequired(String FilePath,String locale = "en")
        {
            var needsUpdate = await DatabaseNeedsUpdate(locale);

            if (needsUpdate.HasValue)
            {
                if (needsUpdate.Value)
                {
                    return await DownloadManifestDatabase(locale, FilePath);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> DownloadManifestDatabase(String locale, String FilePath)
        {
            var manfiest = (await GetDestinyManifest()).Response.mobileWorldContentPaths.Where(x=>x.Key == locale).Select(x=>x.Value).FirstOrDefault();

            if (manfiest != null)
            {
                var zippedDatabase = await _Web.GetStreamAsync(GetIconLink(manfiest));
                using (var file = new FileStream(Path.Combine(FilePath, manfiest.Split('/').Last()), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
                {
                    await zippedDatabase.CopyToAsync(file);
                }
                zippedDatabase.Close();
                return true;
            }
            else
            {
                return false;
            }
        }


        private String FormatComponents(DestinyComponentType[] components)
        {
            var builder = new StringBuilder();

            foreach (var comp in components)
            {
                builder.Append($"{(int)comp},");
            }

            builder[builder.Length - 1] = ' ';

            return builder.ToString();
        }
    }
}
