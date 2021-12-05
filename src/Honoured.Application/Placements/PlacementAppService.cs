using Honoured.Permissions;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Honoured.Placements
{
    public class PlacementAppService : CrudAppService<Placement, PlacementDto, long, GetPlacementListDto,
                                                        CreatePlacementDto, UpdatePlacementDto>
    {
        public PlacementAppService(IRepository<Placement, long> repository) : base(repository)
        {
            GetPolicyName = HonouredPermissions.Placements.Default;
            GetListPolicyName = HonouredPermissions.Placements.Default;
            CreatePolicyName = HonouredPermissions.Placements.Create;
            UpdatePolicyName = HonouredPermissions.Placements.Update;
            DeletePolicyName = HonouredPermissions.Placements.Delete;
        }
    }
}
