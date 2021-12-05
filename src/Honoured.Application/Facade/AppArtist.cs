using Honoured.ArtDisciplines;
using Honoured.Artists;
using Honoured.ArtLovers;
using Honoured.ArtWorks;
using Honoured.Dimensions;
using Honoured.DTOs;
using Honoured.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Honoured.Facade
{
    [Route("AppPortal")]
    public class AppArtist : HonouredAppService
    {
        private ArtistAppService artistService;
        private ArtWorkAppService artworkService;
        private DimensionAppService dimensionService;
        private DisciplineService disciplineService;
        private ArtLoverAppService artLoverService;

        public AppArtist(ArtistAppService artistAppService, ArtWorkAppService artWorkAppService,
                            DimensionAppService dimensionService, DisciplineService disciplineService,
                            ArtLoverAppService artLoverAppService)
        {
            this.artistService = artistAppService;
            this.artworkService = artWorkAppService;
            this.dimensionService = dimensionService;
            this.disciplineService = disciplineService;
            this.artLoverService = artLoverAppService;
        }

        
        [Route("GetPortfolio/{artistId?}")]
        public async Task<PagedResultDto<ArtWorkDto>> GetPortfolio(long? artistId, int skip = 0, int count = int.MaxValue)
        {
            if (!artistId.HasValue)
            {
                throw new Exception("Must provide an artist id");
            }
            var searchDto = new GetArtWorkListDto {
                Filter = artistId.ToString(),
                MaxResultCount = count,
                SkipCount = skip
            };
            return await artworkService.GetPortfolioForArtist(searchDto);
        }

        [Route("NewArtPage")]
        public PagedResultDto<ArtworkPageDto> GetPageInfo()
        {
            var toRet= new PagedResultDto<ArtworkPageDto>
            {
                Items = new List<ArtworkPageDto> {
                new ArtworkPageDto
                        {
                            Dimensions = dimensionService.GetAllActive(),
                            Disciplines = disciplineService.GetAllActive()
                        }},
                TotalCount = 1
            };
            return toRet;
        }


        [Route("Subscriber/{email}")]
        public async Task<SubscriberDto> GetSubscriberAsync(string email)
        {
            
            ArtistDto artistDto = null;
            ArtLoverDto artLoverDto = null;
            try
            {
                artistDto = await artistService.GetArtistByEmail(email);
            }
            catch (Exception) { }
            
            try
            {
                artLoverDto = await artLoverService.GetArtLoverByEmail(email);
            }
            catch (Exception){}
            
            SubscriberDto toRet = GetSubscriberInfo(artistDto, artLoverDto);
            if (toRet == null)
            {
                throw new ArtistNotFoundException(email);
            }
            return toRet;
        }

        private SubscriberDto GetSubscriberInfo(ArtistDto artistDto, ArtLoverDto artLoverDto)
        {
            SubscriberDto toRet = null;
            if (artistDto != null)
            {
                toRet = ObjectMapper.Map<ArtistDto, SubscriberDto>(artistDto);
                toRet.IsArtist = true;
            }
            if(artLoverDto != null)
            {
                if(toRet == null)
                {
                    toRet = ObjectMapper.Map<ArtLoverDto, SubscriberDto>(artLoverDto);
                }
                toRet.IsArtLover = true;
            }
            return toRet;
        }

        [HttpPost]
        [Route("NewArt")]
        public async Task<ArtWorkDto> NewArtwork(CreateArtWorkDto newArt)
        {
            try
            {

                var toRet = await artworkService.CreateAsync(newArt);

                return toRet;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
