using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Honoured.ArtLovers
{
    public interface IArtLoverRepository : IRepository<ArtLover, long>
    {
        //Task<ArtLover> FindArtLoverByKeyAsync(string first, string middle, string last, DateTime dob);

        Task<List<ArtLover>> GetListAsync();

        Task<List<ArtLover>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            ArtLoverStatus filter = ArtLoverStatus.Active
        );

        Task<List<ArtLover>> GetListAsync(Expression<Func<ArtLover, bool>> filter,
                                                            int skipCount = 0, int maxResultCount = 0,
                                                            string sorting = "");
    }
}
