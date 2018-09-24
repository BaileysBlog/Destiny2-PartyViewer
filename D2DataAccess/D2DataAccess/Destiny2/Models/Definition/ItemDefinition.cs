using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Destiny2.Models.Definition
{
    public class ItemDefinition
    {
        public DestinyDisplayPropertiesDefinition displayProperties { get; set; }
        public string secondaryIcon { get; set; }
        public string secondaryOverlay { get; set; }
        public string secondarySpecial { get; set; }
        public DestinyColor backgroundColor { get; set; }
        public string screenshot { get; set; }
        public string itemTypeDisplayName { get; set; }
        public string uilItemDisplayStyle { get; set; }
        public string itemTypeAndTierDisplayName { get; set; }
        public string displaySource { get; set; }
        public UInt32 hash { get; set; }
        public InvetoryDefinition inventory { get; set; }
    }

    public class InvetoryDefinition
    {
        public long bucketTypeHash { get; set; }
    }

    public class DestinyDisplayPropertiesDefinition
    {
        public string description { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public bool hasIcon { get; set; }
    }
}
