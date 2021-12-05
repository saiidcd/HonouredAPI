using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Honoured.Placements
{
    public interface IPlacementAppService : ICrudAppService<PlacementDto, long, GetPlacementListDto, CreatePlacementDto,
                                                            UpdatePlacementDto>
    {
    }
}
