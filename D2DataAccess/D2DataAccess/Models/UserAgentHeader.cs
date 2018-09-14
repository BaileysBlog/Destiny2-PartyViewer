using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Models
{
    public class UserAgentHeader
    {
        public String AppName { get; }
        public String Version { get; }
        public String AppId { get; }
        public int AppIdNum { get; }
        public String WebUrl { get; }
        public String Email { get; }

        public UserAgentHeader(String Name, String Version, String Id, int AppNum, String Web, String Email)
        {
            this.AppName = Name;
            this.Version = Version;
            this.AppId = Id;
            this.AppIdNum = AppNum;
            this.WebUrl = Web;
            this.Email = Email;
        }


        public override string ToString()
        {
            return $"{AppName}/{Version}/{AppIdNum} +({WebUrl};{Email})";
        }
    }
}
