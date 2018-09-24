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

            Console.Title = $"Display Information for {name}";


            var profile = Api.SearchDestinyPlayer(name, BungieMembershipType.TigerBlizzard).Result;

            if (profile.Response != null)
            {
                var player = profile.Response.First();
                var characterBreakdowns = Api.GetCharacterBreakdowns(player.membershipId, BungieMembershipType.TigerBlizzard).Result;
                foreach (var character in characterBreakdowns)
                {
                    Console.WriteLine(
$@"{character.Value.Class}-{character.Value.Race} {character.Value.Gender}
Power-{character.Value.Light} LeveL-{character.Value.LevelProgression.level}

Super       {character.Value.Super.displayProperties.name}
Primary     {character.Value.Primary.displayProperties.name}
Seconday    {character.Value.Secondary.displayProperties.name}
Heavy       {character.Value.Heavy.displayProperties.name}

Helmet      {character.Value.Helm.displayProperties.name}
Arms        {character.Value.Arms.displayProperties.name}
Chest       {character.Value.Chest.displayProperties.name}
Feet        {character.Value.Feet.displayProperties.name}
Class Gear  {character.Value.ClassGear.displayProperties.name}

Current Activity
{(character.Value.CurrentActivity == null ? "Orbit or no activity": character.Value.CurrentActivity.name)}

======================================================================");
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
