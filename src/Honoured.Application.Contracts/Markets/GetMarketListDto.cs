using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Markets
{
    public class GetMarketListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
