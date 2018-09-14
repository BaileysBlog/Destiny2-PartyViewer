using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Enums
{
    public enum PlatformErrorCodes
    {
        None = 0,
        Success = 1,
        TransportException = 2,
        UnhandledException = 3,
        NotImplemented = 4,
        SystemDisabled = 5,
        FailedToLoadAvailableLocalesConfiguration = 6,
        ParameterParseFailure = 7,
        ParameterInvalidRange = 8,
        BadRequest = 9,
        AuthenticationInvalid = 10,
        DataNotFound = 11,
        InsufficientPrivileges = 12,
        Duplicate = 13,
        UnknownSqlResult = 14
    }
}
