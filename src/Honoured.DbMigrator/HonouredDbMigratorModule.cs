using Honoured.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Honoured.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(HonouredEntityFrameworkCoreModule),
        typeof(HonouredApplicationContractsModule)
        )]
    public class HonouredDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
