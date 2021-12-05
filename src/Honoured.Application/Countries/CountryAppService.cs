using Honoured.Permissions;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Honoured.Countries
{
    public class CountryAppService : CrudAppService<Country, CountryDto, long, GetCountryListDto,
                                                         CreateCountryDto, UpdateCountryDto>
    {
        public CountryAppService(IRepository<Country, long> repository) : base(repository)
        {
            GetPolicyName = HonouredPermissions.Countries.Default;
            GetListPolicyName = HonouredPermissions.Countries.Default;
            CreatePolicyName = HonouredPermissions.Countries.Create;
            UpdatePolicyName = HonouredPermissions.Countries.Update;
            DeletePolicyName = HonouredPermissions.Countries.Delete;
        }
    }
}
