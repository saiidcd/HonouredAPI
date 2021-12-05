using Honoured.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Honoured.Subscriptions
{
    public class EfCoreSubscriptionRepository : EfCoreRepository<HonouredDbContext, Subscription, long>,
                                                ISubscriptionRepository
    {

        #region Ctors
        public EfCoreSubscriptionRepository(IDbContextProvider<HonouredDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        #endregion Ctors


        #region Implementations
        public async Task<List<Subscription>> GetDueSubscriptionsBydateAsync(DateTime mindate, DateTime maxDate,
                                                                                int skipCount,
                                                                                    int maxResultCount,
                                                                                    string sorting,
                                                                                    string filter
                                                                                )
        {
            sorting = sorting.IsNullOrWhiteSpace() ? "Id" : sorting;
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(
                    s=>s.NextDeliveryDate.IsBetween(mindate, maxDate)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
        #endregion Implementations
    }
}
