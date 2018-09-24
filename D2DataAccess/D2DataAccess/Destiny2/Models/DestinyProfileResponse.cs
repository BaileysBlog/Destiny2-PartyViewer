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

    }
}
