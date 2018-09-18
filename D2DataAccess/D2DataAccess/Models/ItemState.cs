using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Models
{
    public enum ItemState
    {
        None = 0,
        Locked = 1,
        Tracked = 2,
        Masterwork = 4
    }
}
