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

namespace Honoured.ArtWorks
{
    public class EfCoreArtWorkRepository : EfCoreRepository<HonouredDbContext, ArtWork, long>, IArtWorkRepository
    {

        #region Ctors
        public EfCoreArtWorkRepository(IDbContextProvider<HonouredDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        #endregion Ctors


        #region Implementations
        public async Task<List<ArtWork>> GetPortfolioByArtistId(int skipCount, int maxResultCount,
                                                            string sorting, long artistId)
        {

            sorting = sorting.IsNullOrWhiteSpace() ? "Id" : sorting;
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    artistId>0,
                    a => a.ArtistId==artistId
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
        #endregion Implementations
    }
}
