using D2DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Destiny2.Models.Component_Types
{
    public class BaseComponentResponse
    {
        public ComponentPrivacySetting privacy { get; set; }
        public virtual object data { get; set; }
    }
}
