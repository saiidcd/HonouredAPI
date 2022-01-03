using Honoured.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Honoured.ArtistSubscriptions
{
    public class EfCoreArtistSubscriptionRepository : EfCoreRepository<HonouredDbContext, ArtistSubscription, long>,
            IArtistSubscriptionRepository
    {
        #region Ctors
        public EfCoreArtistSubscriptionRepository(IDbContextProvider<HonouredDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        #endregion Ctors        
    }
}
