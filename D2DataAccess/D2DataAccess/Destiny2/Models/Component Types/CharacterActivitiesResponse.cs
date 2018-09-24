using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Destiny2.Models.Component_Types
{
    public class CharacterActivitiesResponse: BaseComponentResponse
    {
        public new Dictionary<long, CharacterActivitiesComponent> data = new Dictionary<long, CharacterActivitiesComponent>();
    }

    public class CharacterActivitiesComponent
    {
        public DateTime dateActivityStarted { get; set; }
        public List<dynamic> availabeActivities { get; set; }
        public UInt32 currentActivityHash { get; set; }
        public UInt32 currentActivityModeHash { get; set; }
        public object currentActivityModeType { get; set; }
    }
}
