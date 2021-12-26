using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Honoured.ArtistSubscriptions
{
    public interface IArtistSubscriptionService: IApplicationService
    {
        Task<ArtistSubscriptionDto> GetAsync(long id);

        Task<PagedResultDto<ArtistSubscriptionDto>> GetListAsync(GetArtistSubscriptionListDto input);

        Task<ArtistSubscriptionDto> CreateAsync(CreateArtistSubscriptionDto input);

        Task UpdateAsync(long id, UpdateArtistSubscriptionDto input);

        Task DeleteAsync(long id);

    }
}
