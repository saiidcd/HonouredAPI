using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Honoured.Artists
{
    public interface IArtistRepository : IRepository<Artist,long>
    {
        Task<Artist> FindArtisByKeyAsync(string first, string middle, string last, DateTime dob);

        Task<List<Artist>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
        Task<Artist> GetArtistByEmail(string email);
    }
}
