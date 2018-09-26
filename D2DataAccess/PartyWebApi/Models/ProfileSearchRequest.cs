using D2DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyWebApi.Models
{
    public class ProfileSearchRequest
    {
        public String DisplayName { get; set; }
        public BungieMembershipType MembershipType { get; set; }
        public long MembershipId { get; set; }
    }
}