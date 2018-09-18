using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Destiny2.Models.Component_Types
{
    public class CharacterEquipmentResponse : BaseComponentResponse
    {
        public new Dictionary<Int64, InventoryComponent> data = new Dictionary<long, InventoryComponent>();
    }

    public class InventoryComponent
    {
        public List<ItemComponent> items { get; set; } = new List<ItemComponent>();
    }

    public class ItemComponent
    {
        public ItemState state { get; set; }
        public UInt32 itemHash { get; set; }

    }
}
