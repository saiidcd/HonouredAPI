using Microsoft.AspNetCore.Http;
using Honoured.FileUploads;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Net.Http;
using Honoured.DTOs;

namespace Honoured.ArtWorks
{
     public interface IArtWorkAppService : ICrudAppService<ArtWorkDto, //Used to show
        long, //Primary key of the book entity
        GetArtWorkListDto, //Used for paging/sorting on getting a list
        CreateArtWorkDto, //Used to create a new instance
        UpdateArtWorkDto> //Used to update an instance

    {
        Task<PagedResultDto<ArtWorkDto>> GetPortfolioForArtist(GetArtWorkListDto input);

        Task<UploadResult> FileUploadAsync(FileUploadDto fileData);
    }
}
