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

namespace Honoured.Artists
{
    public class EfCoreArtistRepository : EfCoreRepository<HonouredDbContext, Artist, long>,
            IArtistRepository
    {

        #region Ctors
        public EfCoreArtistRepository(IDbContextProvider<HonouredDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        #endregion Ctors


        #region Implementations

        #region IArtistRepository Implementation
        public override async Task<Artist> GetAsync(long id, bool includeDetails = true, 
                                                    CancellationToken cancellationToken = default)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(a=>a.Id==id).Include(a=>a.PersonalDetails).Include(a=>a.DefaultContactPoint).FirstOrDefaultAsync();
        }

        public async Task<Artist> FindArtisByKeyAsync(string first, string middle, string last, DateTime dob)
        {
            var key = $"{first.ToLower()};{middle.ToLower()};{last.ToLower()};{dob.Date.ToShortDateString()}";
            var dbSet = await GetDbSetAsync();
            return await dbSet.Include(a => a.DefaultContactPoint).FirstOrDefaultAsync(artist => artist.Key == key);
        }

        public async Task<List<Artist>> GetListAsync(int skipCount, int maxResultCount,string sorting, string filter = null)
        {
            sorting = sorting.IsNullOrWhiteSpace() ? "Id" : sorting;
            var status = ArtistStatus.Active;
            if (!filter.IsNullOrWhiteSpace())
            {
                Enum.TryParse<ArtistStatus>(filter, out status);
            }

            var dbSet = await GetDbSetAsync();
            var toRet = await dbSet
                .Where(a=> filter.IsNullOrWhiteSpace() ? true : a.Status == status)
                .Include(a=>a.PersonalDetails)
                .Include(a => a.DefaultContactPoint)
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
            return toRet;
        }
        #endregion IArtistRepository Implementation
        #endregion Implementations
    }
}
