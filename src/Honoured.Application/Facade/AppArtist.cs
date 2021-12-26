using Honoured.ArtDisciplines;
using Honoured.Artists;
using Honoured.ArtLovers;
using Honoured.ArtWorks;
using Honoured.Attributes;
using Honoured.Dimensions;
using Honoured.DTOs;
using Honoured.Enumerations;
using Honoured.Models;
using Honoured.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private IHttpContextAccessor _httpContextAccessor;

        public AppArtist(ArtistAppService artistAppService, ArtWorkAppService artWorkAppService,
                            DimensionAppService dimensionService, DisciplineService disciplineService,
                            ArtLoverAppService artLoverAppService, IHttpContextAccessor contextAccessor)
        {
            this.artistService = artistAppService;
            this.artworkService = artWorkAppService;
            this.dimensionService = dimensionService;
            this.disciplineService = disciplineService;
            this.artLoverService = artLoverAppService;
            _httpContextAccessor = contextAccessor;
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

        [Route("Subscriber/Update")]
        public async Task<IActionResult> UpdateSubscriber(SubscriberDto toUpdate)
        {
            if (toUpdate.IsArtist)
            {
                var artist = ObjectMapper.Map<SubscriberDto, ArtistDto>(toUpdate);
                var artistuUpdate = ObjectMapper.Map<ArtistDto, UpdateArtistDto>(artist);
                await artistService.UpdateAsync(toUpdate.ArtistId, artistuUpdate);
            }
            if (toUpdate.IsArtLover)
            {
                var artLover = ObjectMapper.Map<SubscriberDto, ArtLoverDto>(toUpdate);
                var artloverUpdate = ObjectMapper.Map<ArtLoverDto, UpdateArtLoverDto>(artLover);
                await artLoverService.UpdateAsync(toUpdate.Id, artloverUpdate);
            }
            return new NoContentResult();
        }


        [Route("Subscriber/{email}/{size = 1}")]
        public async Task<SubscriberDto> GetSubscriberAsync(string email,int size)
        {
            
            ArtistDto artistDto = null;
            ArtLoverDto artLoverDto = null;
            try
            {
                artistDto = await artistService.GetArtistByEmail(email, size);
            }
            catch (Exception e) { }
            
            try
            {
                artLoverDto = await artLoverService.GetArtLoverByEmail(email);
            }
            catch (Exception e){}
            
            SubscriberDto toRet = GetSubscriberInfo(artistDto, artLoverDto);
            if (toRet == null)
            {
                throw new ArtistNotFoundException(email);
            }
            return toRet;
        }

        [HttpPost]
        [Route("Subscriber/FileUpload")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {
            //TODO make it work for artist and/or artlover
            var Request = _httpContextAccessor.HttpContext.Request;
            if (!RequestUtils.IsMultipartContentType(Request.ContentType))
            {
                return new BadRequestResult();
            }

            var formModel = new FormData();

            var boundary = RequestUtils.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType),
                                                        new FormOptions().MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, Request.Body);
            var section = await reader.ReadNextSectionAsync();
            byte[] streamedFileContent = new byte[0];
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (contentDisposition.IsFileDisposition())
                    {
                        // Don't trust the file name sent by the client. To display the file name, HTML-encode the value.
                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(contentDisposition.FileName.Value);
                        //var trustedFileNameForFileStorage = Path.GetRandomFileName();
                        formModel.TrustedFileName = contentDisposition.FileName.Value;
                        // todo: scan the file's contents using an anti-virus/anti-malware scanner API
                        var Errors = new Dictionary<string, Exception>();
                        streamedFileContent = await FileUtils.ProcessStreamedFile(section, contentDisposition, Errors,
                                                    HonouredAppService.AllowedExtensions, HonouredAppService.MaxFileSize);

                        if (Errors.Any())
                        {
                            throw Errors.Values.First();
                        }
                    }
                    else if (contentDisposition.IsFormDisposition())
                    {
                        var content = new StreamReader(section.Body).ReadToEnd();
                        if (contentDisposition.Name == "artistId" && long.TryParse(content, out var useId))
                        {
                            formModel.UserId = useId;
                        }

                        if (contentDisposition.Name == "artworkId")
                        {
                            formModel.ArtworkId = long.Parse(content);
                        }

                        if (contentDisposition.Name == "email")
                        {
                            formModel.Email = content;
                        }

                        if (contentDisposition.Name == "isPrimary" && bool.TryParse(content, out var isPrimary))
                        {
                            formModel.IsPrimary = isPrimary;
                        }
                    }
                }

                // Drain any remaining section body that hasn't been consumed and read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            var filePath = ImageUtils.GetProfileIconPath(formModel.UserId);
            var trustedFilePath = Path.Combine(filePath, formModel.TrustedFileName);
            Directory.CreateDirectory(filePath);
            using (var targetStream = File.Create(trustedFilePath))
            {
                await targetStream.WriteAsync(streamedFileContent);
            }
            string iconString = ImageUtils.SaveArtistIconAndGetSmallString(IconSize.small, filePath, formModel.UserId, trustedFilePath);
            UpdateSubscriberImagePath(formModel.Email, trustedFilePath);
            return new OkObjectResult(iconString);
        }

        private async void UpdateSubscriberImagePath(string email, string fileName)
        {
            try
            {
                var artistDto = await artistService.GetArtistByEmail(email);
                if (artistDto != null)
                {
                    artistDto.ImageFile = fileName;
                    await artistService.UpdateAsync(artistDto.Id, ObjectMapper.Map<ArtistDto,UpdateArtistDto>(artistDto));
                }
            }
            catch (Exception) { }
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
