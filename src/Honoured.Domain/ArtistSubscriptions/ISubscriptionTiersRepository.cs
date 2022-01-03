using Honoured.Artists;
using Honoured.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Honoured.ArtistSubscriptions
{
    public interface ISubscriptionTiersRepository : IRepository<SubscriptionTier, long>
    {
        Task<List<SubscriptionTier>> GetTiers(bool isActiveOnly = true);
    }
}
