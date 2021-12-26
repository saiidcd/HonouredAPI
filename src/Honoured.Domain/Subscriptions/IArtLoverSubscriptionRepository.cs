using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Honoured.Subscriptions
{
    public interface IArtLoverSubscriptionRepository : IRepository<ArtLoverSubscription,long>
    {
        Task<List<ArtLoverSubscription>> GetDueSubscriptionsBydateAsync(DateTime mindate, DateTime maxDate,
           int skipCount,
           int maxResultCount,
           string sorting,
           string filter
       );
    }
}
