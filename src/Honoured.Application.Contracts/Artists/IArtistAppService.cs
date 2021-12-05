using Honoured.ArtWorks;
using Honoured.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Honoured.Artists
{
    public interface IArtistAppService : IApplicationService
    {
        Task<ArtistDto> GetAsync(long id);

        Task<PagedResultDto<ArtistDto>> GetListAsync(GetArtistListDto input);

        Task<ArtistDto> CreateAsync(CreateArtistDto input);

        Task UpdateAsync(long id, UpdateArtistDto input);

        Task DeleteAsync(long id);

        Task<ArtistDto> GetArtistByEmail(string email);

        Task<UpdateArtistProfileDto> GetArtistProfileByEmail(string email);

        Task UpdateArtistProfileAsync(UpdateArtistProfileDto input);

    }
}
