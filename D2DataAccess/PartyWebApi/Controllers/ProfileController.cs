using D2DataAccess.Destiny2.Models;
using D2DataAccess.Enums;
using D2DataAccess.Simple;
using PartyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PartyWebApi.Controllers
{
    public class ProfileController : ApiController
    {
        [HttpPost]
        public async Task<SearchDestinyPlayerResponse> SearchDestinyPlayer(ProfileSearchRequest data)
        {
            return await WebApiConfig._DestinyApi.SearchDestinyPlayer(data.DisplayName, data.MembershipType);
        }

        [HttpPost]
        public async Task<Dictionary<long, CharacterOverview>> GetCharactersOverview(ProfileSearchRequest data)
        {
            return await WebApiConfig._DestinyApi.GetCharacterBreakdowns(data.MembershipId, data.MembershipType);
        }

        [HttpPost]
        public async Task<CharacterOverview> GetCharacterOverview(long id, ProfileSearchRequest data)
        {
            return await WebApiConfig._DestinyApi.GetCharacterBreakdown(data.MembershipId, data.MembershipType, id);
        }


    }
}
