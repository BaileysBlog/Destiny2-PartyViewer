using D2DataAccess.Data;
using D2DataAccess.Enums;
using D2DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            Assert.IsNotNull(players);
        }
    }
}
