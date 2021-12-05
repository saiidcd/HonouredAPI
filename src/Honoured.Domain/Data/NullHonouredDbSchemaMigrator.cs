using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Honoured.Data
{
    /* This is used if database provider does't define
     * IHonouredDbSchemaMigrator implementation.
     */
    public class NullHonouredDbSchemaMigrator : IHonouredDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}