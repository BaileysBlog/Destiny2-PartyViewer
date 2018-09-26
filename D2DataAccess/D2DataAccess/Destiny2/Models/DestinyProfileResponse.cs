using D2DataAccess.Destiny2.Models.Component_Types;
using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Destiny2.Models
{
    public class DestinyProfileResponse : BaseResponse
    {
        public ComponentDataResponses Response { get; set; }
    }

    public class ComponentDataResponses
    {
        public ProfilesResponse profile { get; set; }
        public CharactersResponse characters { get; set; }
        public CharacterEquipmentResponse characterEquipment { get; set; }
        public CharacterActivitiesResponse characterActivities { get; set; }
        public DestinyItemComponentSetOfint64 itemComponents { get; set; }
        

    }

    public class DictionaryComponentResponseOfint64AndDestinyItemInstanceComponent : BaseComponentResponse
    {
        public new Dictionary<long, DestinyItemInstanceComponent> data = new Dictionary<long, DestinyItemInstanceComponent>();
    }

    public class DestinyItemInstanceComponent
    {
        public Int32 itemLevel { get; set; }
        public bool isEquipped { get; set; }
        public DestinyStat primaryStat { get; set; }

    }
    




    public class DestinyItemComponentSetOfint64
    {
        public DictionaryComponentResponseOfint64AndDestinyItemStatsComponent stats { get; set; }
        public DictionaryComponentResponseOfint64AndDestinyItemInstanceComponent instances { get; set; }
    }

    public class DictionaryComponentResponseOfint64AndDestinyItemStatsComponent : BaseComponentResponse
    {
        public new Dictionary<long, DestinyItemStatsComponent> data = new Dictionary<long, DestinyItemStatsComponent>();
    }

    public class DestinyItemStatsComponent
    {
        public Dictionary<UInt32, DestinyStat> stats = new Dictionary<uint, DestinyStat>();
    }

    public class DestinyStat
    {
        public UInt32 statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }
}
