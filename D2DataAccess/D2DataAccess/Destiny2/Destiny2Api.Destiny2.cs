using D2DataAccess.Destiny2.Models;
using D2DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using D2DataAccess.Extensions;

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

        public async Task<bool> DownloadManifestDatabase(String FilePath)
        {
            return false;
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
