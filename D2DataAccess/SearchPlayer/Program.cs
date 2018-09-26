using D2DataAccess.Data;
using D2DataAccess.Enums;
using D2DataAccess.Models;
using D2DataAccess.Simple;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SearchPlayer
{
    class Program
    {
        protected const string ApiKey = "57a4ba76e897450f8b106635fd20460b";
        static Destiny2Api Api = new Destiny2Api(ApiKey, 
            new UserAgentHeader("Destiny 2 Party Viewer", "1.0.0", "Console Program", 0, "https://www.d2-partyviewer.com", "baileymiller@live.com"));

        static string TrackedProfile;
        static void Main(string[] args)
        {
            Console.Title = "Destiny 2 Searcher";
            
            Api.UpdateDatabaseIfRequired().Wait();
            
            SetupStartingLoop();
        }

        static void SetupStartingLoop()
        {
            Console.WriteLine("Enter a PC name to begin tracking the active character");
            TrackedProfile = Console.ReadLine();

            Console.Title = $"Display Information for {TrackedProfile}";
            
            Console.CancelKeyPress += CloseConsole;

            DisplayAccountInformation();
        }

        private static void CloseConsole(object sender, ConsoleCancelEventArgs e)
        {
            Console.Clear();

            Console.WriteLine("Search New Account? Y/N");
            var key = Console.ReadKey().Key;

            if (key == ConsoleKey.Y)
            {
                Console.Clear();
                SetupStartingLoop();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Closing in 2 seconds . . .");
                Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                Environment.Exit(0);
            }
        }
       

        static void DisplayAccountInformation()
        {
            var profile = Api.SearchDestinyPlayer(TrackedProfile, BungieMembershipType.TigerBlizzard).Result;

            if (profile.Response != null && profile.Response.Count != 0)
            {
                var player = profile.Response.First();
                var characterBreakdowns = Api.GetCharacterBreakdowns(player.membershipId, BungieMembershipType.TigerBlizzard).Result;

                if (characterBreakdowns.Count(x => x.Value.CurrentActivity == null) == characterBreakdowns.Count)
                {
                    Console.WriteLine("Either character(s) are in orbit or not active");
                    LogCharacterData(characterBreakdowns);
                }
                else
                {
                    var watchedCharacter = characterBreakdowns.First(x => x.Value.CurrentActivity != null);
                    LogCharacterData(new Dictionary<long, CharacterOverview>() { {watchedCharacter.Key, watchedCharacter.Value } });
                }
            }
            else
            {
                // Dead
                SetupStartingLoop();
            }
            Console.WriteLine($"Updating at {DateTime.Now.AddMinutes(1).ToLongTimeString()}");
            Task.Delay(TimeSpan.FromMinutes(1)).Wait();
            
            DisplayAccountInformation();
        }

        static void LogCharacterData(Dictionary<long, CharacterOverview> characterBreakdowns)
        {
            Console.Clear();
            foreach (var character in characterBreakdowns)
            {
                Console.WriteLine(
$@"{character.Value.Class}-{character.Value.Race} {character.Value.Gender}
Power-{character.Value.Light} LeveL-{character.Value.LevelProgression.level}

Super       {character.Value.Super.displayProperties.name}
Primary     {character.Value.Primary.displayProperties.name} ({character.Value.Primary.ItemInstanceInformation.primaryStat.value})
Seconday    {character.Value.Secondary.displayProperties.name} ({character.Value.Secondary.ItemInstanceInformation.primaryStat.value})
Heavy       {character.Value.Heavy.displayProperties.name} ({character.Value.Heavy.ItemInstanceInformation.primaryStat.value})

Helmet      {character.Value.Helm.displayProperties.name} ({character.Value.Helm.ItemInstanceInformation.primaryStat.value})
Arms        {character.Value.Arms.displayProperties.name} ({character.Value.Arms.ItemInstanceInformation.primaryStat.value})
Chest       {character.Value.Chest.displayProperties.name} ({character.Value.Chest.ItemInstanceInformation.primaryStat.value})
Feet        {character.Value.Feet.displayProperties.name} ({character.Value.Feet.ItemInstanceInformation.primaryStat.value})
Class Gear  {character.Value.ClassGear.displayProperties.name} ({character.Value.ClassGear.ItemInstanceInformation.primaryStat.value})

Current Activity
{(character.Value.CurrentActivity == null ? "Orbit or no activity" : character.Value.CurrentActivity.name)}
{(characterBreakdowns.Count == 1 ? "": "\n======================================================================")}");
            }
        }
    }
}
