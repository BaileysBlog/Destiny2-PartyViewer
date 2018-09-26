using D2DataAccess.Destiny2.Models;
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

                data.Add(character.Key, new CharacterOverview()
                {
                    Light = character.Value.light,
                    LevelProgression = character.Value.levelProgression,
                    Class = className,
                    Race = raceName,
                    Gender = genderName,
                    Super = await GetItemInformation("Subclass", characterEquipment, characters.Response.itemComponents.instances),
                    Helm = await GetItemInformation("Helmet", characterEquipment, characters.Response.itemComponents.instances),
                    Arms = await GetItemInformation("Gauntlets", characterEquipment, characters.Response.itemComponents.instances),
                    Chest = await GetItemInformation("Chest Armor", characterEquipment, characters.Response.itemComponents.instances),
                    Feet = await GetItemInformation("Leg Armor", characterEquipment, characters.Response.itemComponents.instances),
                    ClassGear = await GetItemInformation("Class Armor", characterEquipment, characters.Response.itemComponents.instances),
                    Primary = await GetItemInformation("Kinetic Weapons", characterEquipment, characters.Response.itemComponents.instances),
                    Secondary = await GetItemInformation("Energy Weapons", characterEquipment, characters.Response.itemComponents.instances),
                    Heavy = await GetItemInformation("Power Weapons", characterEquipment, characters.Response.itemComponents.instances),
                    Ghost = await GetItemInformation("Ghost", characterEquipment, characters.Response.itemComponents.instances),
                    Emblem = await GetItemInformation("Emblems", characterEquipment, characters.Response.itemComponents.instances),
                    CurrentActivity = activityDisplay == null ? null : JsonConvert.DeserializeObject<DestinyDisplayPropertiesDefinition>(JsonConvert.SerializeObject(activityDisplay))
                });
            }

            return data;
        }
        
        private async Task<ItemDefinition> GetItemInformation(String ItemFieldName, IEnumerable<dynamic> characterEquipment, DictionaryComponentResponseOfint64AndDestinyItemInstanceComponent instancedItems)
        {
            try
            {
                var items = await DataEngine.GetDetailedInformationFromHash<ItemDefinition>(DestinyTable.ItemDefinition, characterEquipment.Select(x => (ulong)x.itemHash).ToArray());
                var itemHash = (await DataEngine.GetTableDump<dynamic>(DestinyTable.InventoryBucketDefinition))
                        .Where(x => x.Value.displayProperties.name == ItemFieldName).Select(x => x.Key).First();

                var item = items.Where(x => x.Value.inventory.bucketTypeHash == itemHash).Select(x => x.Value).First();

                var intaItem = (long)(characterEquipment.Where(x => x.itemHash == item.hash).Select(x => x.itemInstanceId).First());

                var instanceItem = instancedItems.data.Where(x => x.Key == intaItem).Select(x => x.Value).First();

                if (instanceItem.primaryStat != null)
                {
                    var statName = (await DataEngine.GetDetailedInformationFromHash<dynamic>(DestinyTable.StatDefinition, instanceItem.primaryStat.statHash)).First().Value.displayProperties.name;

                    instanceItem.primaryStat.StatName = statName;
                }
                item.ItemInstanceInformation = instanceItem;

                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
