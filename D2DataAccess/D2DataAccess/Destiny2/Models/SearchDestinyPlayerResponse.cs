using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Destiny2.Models
{
    public class SearchDestinyPlayerResponse: BaseResponse
    {
        public List<UserInfoCard> Response { get; set; }
    }
}
