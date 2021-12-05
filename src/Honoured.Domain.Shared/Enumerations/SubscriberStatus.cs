using System;
using System.Collections.Generic;
using System.Text;

namespace Honoured.Enumerations
{
    [Flags]
    public enum ArtLoverStatus
    {
        Pending =0,
        Active=1,
        Suspended=2,
        Deleted=4
    }
}
