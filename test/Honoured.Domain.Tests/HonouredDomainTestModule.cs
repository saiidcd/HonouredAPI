using Honoured.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Honoured
{
    [DependsOn(
        typeof(HonouredEntityFrameworkCoreTestModule)
        )]
    public class HonouredDomainTestModule : AbpModule
    {

    }
}