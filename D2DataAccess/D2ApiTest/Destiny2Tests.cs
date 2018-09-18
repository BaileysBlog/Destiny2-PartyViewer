using D2DataAccess.Data;
using D2DataAccess.Enums;
using D2DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2ApiTest
{

    [TestClass]
    public class Destiny2Tests: BaseTest
    {
        [TestMethod]
        public async Task SearchDestinyPlayer()
        {
            var players = await Api.SearchDestinyPlayer("Breadstick#11371", BungieMembershipType.TigerBlizzard);
            Assert.IsTrue(players.ErrorCode != PlatformErrorCodes.ApiInvalidOrExpiredKey, $"Api Key Error: {players.Message}");
            Assert.IsNotNull(players);
            Assert.IsNotNull(players.Response);
        }

        [TestMethod]
        public async Task GetProfile()
        {
            var bailey = (await Api.SearchDestinyPlayer("Breadstick#11371", BungieMembershipType.TigerBlizzard)).Response.First();
            var profile = await Api.GetProfile(bailey.membershipId, bailey.membershipType, DestinyComponentType.Profiles, DestinyComponentType.Characters, DestinyComponentType.CharacterEquipment);
            Assert.IsTrue(profile.ErrorCode != PlatformErrorCodes.ApiInvalidOrExpiredKey, $"Api Key Error: {profile.Message}");

        }
    }
}
