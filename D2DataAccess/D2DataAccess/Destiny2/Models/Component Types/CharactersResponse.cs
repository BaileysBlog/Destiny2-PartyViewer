using D2DataAccess.Enums;
using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Destiny2.Models.Component_Types
{
    public class CharactersResponse : BaseComponentResponse
    {
        public new Dictionary<Int64, CharacterComponent> data { get; set; } = new Dictionary<long, CharacterComponent>();
    }

    public class CharacterComponent
    {
        public Int64 membershipId { get; set; }
        public BungieMembershipType membershipType { get; set; }
        public Int64 characterId { get; set; }
        public DateTime dateLastPlayed { get; set; }
        public Int64 minutesPlayedThisSession { get; set; }
        public Int64 minutesPlayedTotal { get; set; }
        public Int32 light { get; set; }
        public Dictionary<UInt32, Int32> stats = new Dictionary<uint, int>();
        public UInt32 classHash { get; set; }
        public string emblemPath { get; set; }
        public string emblemBackgroundPath { get; set; }
        public UInt32 emblemHash { get; set; }
        public DestinyColor emblemColor { get; set; }
        public DestinyProgression levelProgression { get; set; }
        public Int32 baseCharacterLevel { get; set; }
        public float percentToNextLevel { get; set; }

    }
}
