using Honoured.EntityFrameworkCore;
using Honoured.Enumerations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Threading;
using System.Linq.Expressions;

namespace Honoured.ArtLovers
{
    public class EfCoreArtLoverRepository : EfCoreRepository<HonouredDbContext, ArtLover, long>,
                                                IArtLoverRepository
    {
        #region Ctors
        public EfCoreArtLoverRepository(IDbContextProvider<HonouredDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        #endregion Ctors


        #region Implementations

        #region IArtLoverRepository Implementation
        public override async Task<ArtLover> GetAsync(long id, bool includeDetails = true,
                                                    CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(a => a.Id == id).Include(a => a.Profile).FirstOrDefaultAsync();
        }
        public async Task<List<ArtLover>> GetListAsync() 
                    => await GetListAsync(0, 0, "", EnumFlags.AllArtLoverStatus);

        public async Task<List<ArtLover>> GetListAsync(int skipCount, int maxResultCount, string sorting,
                                                            ArtLoverStatus filter = ArtLoverStatus.Active)
        {
            Expression<Func<ArtLover, bool>> StatusFilter = (a => (a.Status & filter) == a.Status);
            return await GetListAsync(StatusFilter, skipCount, maxResultCount, sorting);
        }

        public async Task<List<ArtLover>> GetListAsync(Expression<Func<ArtLover, bool>> filter, 
                                                            int skipCount = 0, int maxResultCount = 0,
                                                            string sorting = "")
        {
            if (maxResultCount == 0) maxResultCount = int.MaxValue;
            sorting = sorting.IsNullOrWhiteSpace() ? "Id" : sorting;
            if (filter == null)
            {
                filter = f=>true;
            }
            var dbSet = await GetDbSetAsync();
            var toRet = await dbSet
                .Where(filter)
                .Include(a => a.Profile)
                .Include(b=>b.Categories)
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
            return toRet;
        }
        #endregion IArtLoverRepository Implementation
        #endregion Implementations
    }
}
