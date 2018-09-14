using D2DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.User.Models
{
    public class SearchUsersResponse: BaseResponse
    {
        public List<GeneralUser> Response { get; set; }
    }
}
