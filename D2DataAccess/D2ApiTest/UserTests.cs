using D2DataAccess.Data;
using D2DataAccess.Enums;
using D2DataAccess.Models;
using D2DataAccess.User.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace D2ApiTest
{
    [TestClass]
    public class UserTests: BaseTest
    {
        [TestMethod]
        public async Task SearchDestinyPlayer()
        {
            var bailey = await Api.SearchUsers("Holy Breadstick");
            Assert.IsTrue(bailey.ErrorCode != PlatformErrorCodes.ApiInvalidOrExpiredKey, $"Api Key Error: {bailey.Message}");
            Assert.IsNotNull(bailey);
        }

        [TestMethod]
        public async Task GetBungieNetUserById()
        {
            var bailey = await Api.GetBungieNetUserById(303575);
            Assert.IsTrue(bailey.ErrorCode != PlatformErrorCodes.ApiInvalidOrExpiredKey, $"Api Key Error: {bailey.Message}");
            Assert.IsNotNull(bailey, "Was expecting value");
        }

        [TestMethod]
        public async Task SearchNameAndIdReturnSameData()
        {
            var byId = (await Api.GetBungieNetUserById(303575)).Response.membershipId;
            var bySearch = (await Api.SearchUsers("Holy Breadstick")).Response.First().membershipId;
            Assert.AreEqual(byId, bySearch, $"ById: {byId} BySearch: {bySearch}");
        }



        
    }
}
