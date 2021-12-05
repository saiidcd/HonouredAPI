using Volo.Abp.Modularity;

namespace Honoured
{
    [DependsOn(
        typeof(HonouredApplicationModule),
        typeof(HonouredDomainTestModule)
        )]
    public class HonouredApplicationTestModule : AbpModule
    {

    }
}