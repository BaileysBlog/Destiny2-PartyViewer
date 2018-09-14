using D2DataAccess.Enums;
using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Destiny2.Models
{
    public class UserInfoCard
    {
        public String supplementalDisplayName { get; set; }
        public String iconPath { get; set; }
        public BungieMembershipType membershipType { get; set; }
        public Int64 membershipId { get; set; }
        public String displayName { get; set; }
    }
}
