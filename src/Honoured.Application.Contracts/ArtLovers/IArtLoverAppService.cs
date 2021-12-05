using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Honoured.ArtLovers
{
    public interface IArtLoverAppService : IApplicationService
    {
        Task<PagedResultDto<ArtLoverDto>> GetListAsync(GetArtLoversListDto input);

        Task<ArtLoverDto> GetAsync(long id);

        Task<ArtLoverDto> CreateAsync(CreateArtLoverDto input);

        Task UpdateAsync(long id, UpdateArtLoverDto input);

        Task DeleteAsync(long id);

        Task<ArtLoverDto> GetArtLoverByEmail(string email);
    }
}
