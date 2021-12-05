using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtDisciplines
{
    public class GetDisciplineListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
