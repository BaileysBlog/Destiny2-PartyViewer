using D2DataAccess.Destiny2.Models.Definition;
using D2DataAccess.Enums;
using D2DataAccess.Simple;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2DataAccess.Data
{
    public partial class Destiny2Api
    {
        public async Task<CharacterOverview> GetCharacterBreakdown(long membershipId, BungieMembershipType membershipType, long characterId)
        {
            var search = await GetCharacterBreakdowns(membershipId, membershipType);
            return search.Where(x => x.Key == characterId).Select(x=>x.Value).First();
        }

        public async Task<Dictionary<long, CharacterOverview>> GetCharacterBreakdowns(long membershipId, BungieMembershipType membershipType)
        {

            Dictionary<long, CharacterOverview> data = new Dictionary<long, CharacterOverview>();

            var characters = await GetProfile(membershipId, membershipType, 
                DestinyComponentType.Characters, 
                DestinyComponentType.CharacterEquipment, 
                DestinyComponentType.CharacterActivities, 
                DestinyComponentType.ItemStats,
                DestinyComponentType.ItemInstances);


            foreach (var character in characters.Response.characters.data)
            {
                var curActivity = characters.Response.characterActivities.data.Where(x => x.Key == character.Key).Select(x => x.Value).First();
                var characterEquipment = characters.Response.characterEquipment.data.Where(x => x.Key == character.Key).FirstOrDefault().Value.items.Select(x => new { x.itemHash, x.itemInstanceId });
                var items = await DataEngine.GetDetailedInformationFromHash<ItemDefinition>(DestinyTable.ItemDefinition, characterEquipment.Select(x=>x.itemHash).ToArray());
                var characterStats = characters.Response.characters.data.Where(x => x.Key == character.Key).FirstOrDefault();


                
                var activityDisplay = (await DataEngine.GetDetailedInformationFromHash<dynamic>(DestinyTable.ActivityDefinition, curActivity.currentActivityHash))
                    .Select(x=>x.Value.displayProperties).FirstOrDefault();
                
                var className = (await DataEngine.GetDetailedInformationFromHash<dynamic>(DestinyTable.ClassDefinition, character.Value.classHash))
                    .Where(x=>x.Key == character.Value.classHash)
                    .Select(x=>x.Value)
                    .First().displayProperties.name;

                var raceName = (await DataEngine.GetDetailedInformationFromHash<dynamic>(DestinyTable.RaceDefinition, character.Value.raceHash))
                    .Where(x => x.Key == character.Value.raceHash)
                    .Select(x => x.Value)
                    .First().displayProperties.name;

                var genderName = (await DataEngine.GetDetailedInformationFromHash<dynamic>(DestinyTable.GenderDefinition, character.Value.genderHash))
                    .Where(x => x.Key == character.Value.genderHash)
                    .Select(x => x.Value)
                    .First().displayProperties.name;


                var helmHash = (await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                    .Where(x => x.Value.displayProperties.name == "Helmet").Select(x => x.Key).First();

                var armsHash = (await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                    .Where(x => x.Value.displayProperties.name == "Gauntlets").Select(x => x.Key).First();

                var chestHash = (await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                    .Where(x=>x.Value.displayProperties.name == "Chest Armor").Select(x=>x.Key).First();

                var feetHash =(await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                    .Where(x => x.Value.displayProperties.name == "Leg Armor").Select(x => x.Key).First();

                var classGearHash = (await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                    .Where(x => x.Value.displayProperties.name == "Class Armor").Select(x => x.Key).First();

                var primaryHash = (await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                    .Where(x => x.Value.displayProperties.name == "Kinetic Weapons").Select(x => x.Key).First();

                var secondaryHash = (await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                    .Where(x => x.Value.displayProperties.name == "Energy Weapons").Select(x => x.Key).First();

                var heavyHash = (await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                    .Where(x => x.Value.displayProperties.name == "Power Weapons").Select(x => x.Key).First();


                var emblemHash = (await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                    .Where(x => x.Value.displayProperties.name == "Emblems").Select(x => x.Key).First();

                var ghostHash = (await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                    .Where(x => x.Value.displayProperties.name == "Ghost").Select(x => x.Key).First();

                var superHash = (await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                    .Where(x => x.Value.displayProperties.name == "Subclass").Select(x => x.Key).First();


                var pItemDef = items.Where(x => x.Value.inventory.bucketTypeHash == primaryHash).Select(x => x.Value).First();

                var intaItem = characterEquipment.Where(x => x.itemHash == pItemDef.hash).Select(x => x.itemInstanceId).First();

                var primaryData = new {
                    ItemDef = pItemDef,
                    InstancedItemHash = intaItem,
                    Info = characters.Response.itemComponents.instances.data.Where(x=>x.Key == intaItem.Value).Select(x=>x.Value.primaryStat)
                };


                data.Add(character.Key, new CharacterOverview()
                {
                    Light = character.Value.light,
                    LevelProgression = character.Value.levelProgression,
                    Class = className,
                    Race = raceName,
                    Gender = genderName,
                    Super = items.Where(x => x.Value.inventory.bucketTypeHash == superHash).Select(x => x.Value).First(),
                    Helm = items.Where(x => x.Value.inventory.bucketTypeHash == helmHash).Select(x => x.Value).First(),
                    Arms = items.Where(x => x.Value.inventory.bucketTypeHash == armsHash).Select(x => x.Value).First(),
                    Chest = items.Where(x => x.Value.inventory.bucketTypeHash == chestHash).Select(x => x.Value).First(),
                    Feet = items.Where(x => x.Value.inventory.bucketTypeHash == feetHash).Select(x => x.Value).First(),
                    ClassGear = items.Where(x => x.Value.inventory.bucketTypeHash == classGearHash).Select(x => x.Value).First(),
                    Primary = items.Where(x => x.Value.inventory.bucketTypeHash == primaryHash).Select(x => x.Value).First(),
                    Secondary = items.Where(x => x.Value.inventory.bucketTypeHash == secondaryHash).Select(x => x.Value).First(),
                    Heavy = items.Where(x => x.Value.inventory.bucketTypeHash == heavyHash).Select(x => x.Value).First(),
                    Ghost = items.Where(x => x.Value.inventory.bucketTypeHash == ghostHash).Select(x => x.Value).First(),
                    Emblem = items.Where(x => x.Value.inventory.bucketTypeHash == emblemHash).Select(x => x.Value).First(),
                    CurrentActivity = activityDisplay == null ? null : JsonConvert.DeserializeObject<DestinyDisplayPropertiesDefinition>(JsonConvert.SerializeObject(activityDisplay))
                });
            }

            return data;
        }
    }
}
