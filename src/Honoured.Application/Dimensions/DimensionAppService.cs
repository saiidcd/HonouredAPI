using Honoured.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Honoured.Dimensions
{
    public class DimensionAppService : CrudAppService<Dimension, DimensionDto, long, GetDimensionListDto,
                                                        CreateDimensionDto, UpdateDimensionDto>
    {
        public DimensionAppService(IRepository<Dimension, long> repository) : base(repository)
        {
            GetPolicyName = HonouredPermissions.Dimensions.Default;
            GetListPolicyName = HonouredPermissions.Dimensions.Default;
            CreatePolicyName = HonouredPermissions.Dimensions.Create;
            UpdatePolicyName = HonouredPermissions.Dimensions.Update;
            DeletePolicyName = HonouredPermissions.Dimensions.Delete;
        }

        public List<DimensionDto> GetAllActive()
        {
            var models = Repository.GetListAsync(d => d.Status == Enumerations.GeneralStatus.Active).GetAwaiter().GetResult();
            return ObjectMapper.Map<List<Dimension>, List<DimensionDto>>(models);
        }
    }
}
