using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Dimensions
{
    public class GetDimensionListDto : PagedAndSortedResultRequestDto
    {
        public GeneralStatus Filter { get; set; }
    }
}
