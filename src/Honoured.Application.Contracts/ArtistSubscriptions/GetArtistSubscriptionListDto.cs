using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtistSubscriptions
{
    public class GetArtistSubscriptionListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
