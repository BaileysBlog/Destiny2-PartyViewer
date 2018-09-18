using D2DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Destiny2.Models.Component_Types
{
    public class ProfilesResponse : BaseComponentResponse
    {
        public new ProfileComponent data { get; set; }
    }

    public class ProfileComponent
    {
        public UserInfoCard userinfo { get; set; }
        public DateTime dateLastPlayed { get; set; }
        public DestinyGameVersions versionsOwned { get; set; }
        public List<Int64> characterIds { get; set; } = new List<long>();
    }
}
