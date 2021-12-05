using Honoured.Permissions;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Honoured.Markets
{
    //[ExposeServices(typeof(IArtistAreaAppService))]
    public class MarketAppService : CrudAppService<Market, MarketDto, long, GetMarketListDto,
                                                        CreateMarketDto, UpdateMarketDto>
    {
        public MarketAppService(IRepository<Market, long> repository) : base(repository)
        {
            GetPolicyName = HonouredPermissions.Markets.Default;
            GetListPolicyName = HonouredPermissions.Markets.Default;
            CreatePolicyName = HonouredPermissions.Markets.Create;
            UpdatePolicyName = HonouredPermissions.Markets.Update;
            DeletePolicyName = HonouredPermissions.Markets.Delete;
        }
    }
}
