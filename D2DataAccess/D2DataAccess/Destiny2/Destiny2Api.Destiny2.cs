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
    }
}
