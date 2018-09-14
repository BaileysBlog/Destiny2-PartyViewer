using D2DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Models
{
    public class BaseResponse
    {
        public PlatformErrorCodes ErrorCode { get; set; }
        public Int32 ThrottleSeconds { get; set; }
        public String ErrorStatus { get; set; }
        public String Message { get; set; }
        public Dictionary<String, String> MessageData { get; set; }
        public String DetailedErrorTrace { get; set; }

    }
}
