using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtLovers
{
    public class GetArtLoversListDto : PagedAndSortedResultRequestDto
    {
        public ArtLoverStatus Filter { get; set; } = ArtLoverStatus.Active | ArtLoverStatus.Deleted
            | ArtLoverStatus.Pending | ArtLoverStatus.Suspended;
    }
}
