using System.Threading.Tasks;

namespace Honoured.Data
{
    public interface IHonouredDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
