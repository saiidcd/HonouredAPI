using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Honoured.Data;
using Volo.Abp.DependencyInjection;

namespace Honoured.EntityFrameworkCore
{
    public class EntityFrameworkCoreHonouredDbSchemaMigrator
        : IHonouredDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreHonouredDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the HonouredDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<HonouredDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
