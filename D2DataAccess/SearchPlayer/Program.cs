using D2DataAccess.Data;
using D2DataAccess.Enums;
using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPlayer
{
    class Program
    {
        protected const string ApiKey = "57a4ba76e897450f8b106635fd20460b";
        static Destiny2Api Api = new Destiny2Api(ApiKey, new UserAgentHeader("Destiny 2 Party Viewer", "1.0.0", "Console Program", 0, "https://www.d2-partyviewer.com", "baileymiller@live.com"), @"C:\Users\Bailey Miller\Desktop\Destiny 2 Manifest\worldAssets\world.content");
        static void Main(string[] args)
        {
            Console.Title = "Destiny 2 Searcher";

            Console.WriteLine("Enter a PC name to get the character list");
            var name = Console.ReadLine();


            var profile = Api.SearchDestinyPlayer(name, BungieMembershipType.TigerBlizzard).Result;

            if (profile.Response != null)
            {
                var player = profile.Response.First();
                var characters = (Api.GetProfile(player.membershipId, BungieMembershipType.TigerBlizzard, DestinyComponentType.Characters).Result).Response.characters.data;
                foreach (var character in characters)
                {
                    var classes = Api.DataEngine.GetTableDump<dynamic>(DestinyTable.ClassDefinition).Result;
                    var _class = classes.Where(x => x.Value.hash == character.Value.classHash);
                    Console.WriteLine($"{_class.First().Value.displayProperties.name} {character.Value.light}");
                }
            }
            else
            {
                // Dead
            }


            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
