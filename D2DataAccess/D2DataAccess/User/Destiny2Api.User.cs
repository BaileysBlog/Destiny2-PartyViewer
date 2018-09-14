using D2DataAccess.Enums;
using D2DataAccess.User.Models;
using D2DataAccess.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace D2DataAccess.Data
{
    public partial class Destiny2Api
    {
        
        public async Task<GetBungieNetUserByIdResponse> GetBungieNetUserById(Int64 Id)
        {
            string path = $"User/GetBungieNetUserById/{Id}/";
            return await _Web.GetAsync<GetBungieNetUserByIdResponse>(path).ConfigureAwait(false);
        }

        public async Task<SearchUsersResponse> SearchUsers(String q)
        {
            string path = $"User/SearchUsers/?q={q}";

            return await _Web.GetAsync<SearchUsersResponse>(path, false).ConfigureAwait(false);
        }
    }
}
