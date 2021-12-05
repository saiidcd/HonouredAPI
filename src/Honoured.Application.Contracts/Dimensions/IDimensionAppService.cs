using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Honoured.Dimensions
{
    public interface IDimensionAppService : ICrudAppService<DimensionDto, long, GetDimensionListDto, CreateDimensionDto, UpdateDimensionDto>
    {
        public List<DimensionDto> GetAllActive();
    }
}
