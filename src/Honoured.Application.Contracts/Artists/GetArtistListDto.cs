using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Artists
{
    public class GetArtistListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
