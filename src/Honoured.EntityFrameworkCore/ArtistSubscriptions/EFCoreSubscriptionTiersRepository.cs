using Honoured.Artists;
using Honoured.Dimensions;
using Honoured.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Honoured.ArtistSubscriptions
{
    class EFCoreSubscriptionTiersRepository : EfCoreRepository<HonouredDbContext, SubscriptionTier, long>,
            ISubscriptionTiersRepository
    {
        public EFCoreSubscriptionTiersRepository(IDbContextProvider<HonouredDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<SubscriptionTier>> GetTiers(bool isActiveOnly = true)
        {
            var ct = await GetDbContextAsync();
            return await ct.SubscriptionTiers
                .Include(a => a.MaxDimension)
                .Where(a => a.Status == Enumerations.GeneralStatus.Active)
                .ToListAsync();
        }
    }
}
