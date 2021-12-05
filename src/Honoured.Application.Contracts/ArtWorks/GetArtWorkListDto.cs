using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtWorks
{
    public class GetArtWorkListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
