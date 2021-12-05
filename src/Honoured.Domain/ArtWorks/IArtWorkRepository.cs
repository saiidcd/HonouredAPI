using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Honoured.ArtWorks
{
    public interface IArtWorkRepository : IRepository<ArtWork,long>
    {
        Task<List<ArtWork>> GetPortfolioByArtistId(
            int skipCount,
            int maxResultCount,
            string sorting,
            long artistId
        );
    }
}
