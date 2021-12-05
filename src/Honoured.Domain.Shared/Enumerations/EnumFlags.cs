using System;
using System.Collections.Generic;
using System.Text;

namespace Honoured.Enumerations
{
    public static class EnumFlags
    {
        public static readonly ArtLoverStatus AllArtLoverStatus =
                                                                        ArtLoverStatus.Active |
                                                                        ArtLoverStatus.Deleted |
                                                                        ArtLoverStatus.Pending |
                                                                        ArtLoverStatus.Suspended;
    }
}
