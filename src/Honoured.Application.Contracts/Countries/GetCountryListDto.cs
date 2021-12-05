using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Countries
{
    public class GetCountryListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
