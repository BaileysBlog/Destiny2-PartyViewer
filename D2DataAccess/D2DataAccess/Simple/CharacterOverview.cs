using D2DataAccess.Destiny2.Models.Definition;
using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Simple
{
    public class CharacterOverview
    {
        public String Class { get; set; }
        public String Race { get; set; }
        public String Gender { get; set; }
        public int Light { get; set; }
        public DestinyProgression LevelProgression { get; set; }
        public ItemDefinition Emblem { get; set; }

        public ItemDefinition Super { get; set; }

        public ItemDefinition Primary { get; set; }
        public ItemDefinition Secondary { get; set; }
        public ItemDefinition Heavy { get; set; }
        public ItemDefinition Helm { get; set; }
        public ItemDefinition Arms { get; set; }
        public ItemDefinition Chest { get; set; }
        public ItemDefinition Feet { get; set; }
        public ItemDefinition ClassGear { get; set; }

        public ItemDefinition Ghost { get; set; }
        public ItemDefinition Sparrow { get; set; }
        public ItemDefinition Ship { get; set; }

        public DestinyDisplayPropertiesDefinition CurrentActivity { get; set; }

        public string BungieRoot { get; set; } = "https://www.bungie.net";
    }
    
}
