using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Honoured.ArtistSubscriptions
{
    public interface IArtistSubscriptionRepository : IRepository<ArtistSubscription, long>
    {

    }
}
