using Honoured.Artists;
using Honoured.ArtWorks;
using Honoured.Attributes;
using Honoured.DTOs;
using Honoured.Enumerations;
using Honoured.FileUploads;
using Honoured.Models;
using Honoured.Permissions;
using Honoured.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Honoured.Artists
{
    [Route("Artist")]
    [Authorize(HonouredPermissions.Artists.Default)]
    //[AllowAnonymous]
    public class ArtistAppService : HonouredAppService, IArtistAppService
    {

        #region Fields
        private IArtistRepository _artistRepository;
        private ArtistManager _artistManager;
        private IWebHostEnvironment _env;
        private IHttpContextAccessor _httpContextAccessor;

        #endregion Fields

        #region Ctors
        public ArtistAppService(IWebHostEnvironment env, IArtistRepository repo, ArtistManager mgr,
            IHttpContextAccessor httpContextAccessor)
        {
            _artistRepository = repo;
            _artistManager = mgr;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion Ctors


        #region Implementations

        [Authorize(HonouredPermissions.Artists.Create)]
        public async Task<ArtistDto> CreateAsync(CreateArtistDto input)
        {
            var artist = await _artistManager.CreateAsync(input.First, input.Middle, input.Last, input.DOB);

            await _artistRepository.InsertAsync(artist);
            return ObjectMapper.Map<Artist, ArtistDto>(artist);
        }

        [Authorize(HonouredPermissions.Disciplines.Delete)]
        public async Task DeleteAsync(long id)
        {
            await _artistRepository.DeleteAsync(id);
        }
        [AllowAnonymous]
        public async Task<ArtistDto> GetAsync(long id)
        {
            var artist = await _artistRepository.GetAsync(id);
            var toRet = ObjectMapper.Map<Artist, ArtistDto>(artist);
            toRet.Last = $"{CurrentUser?.Id}" ;
            return toRet;
        }

        public async Task<PagedResultDto<ArtistDto>> GetListAsync(GetArtistListDto input)
        {
            var artists = await _artistRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );
            //var totalCount = 2;
            var totalCount = input.Filter == null
                ? await _artistRepository.CountAsync()
                : await _artistRepository.CountAsync(
                    author => author.Key.Contains(input.Filter));

            return new PagedResultDto<ArtistDto>(
                totalCount,
                ObjectMapper.Map<List<Artist>, List<ArtistDto>>(artists)
            );
        }

        [Authorize(HonouredPermissions.Artists.Update)]
        public async Task UpdateAsync(long id, UpdateArtistDto input)
        {
            var artist = await _artistRepository.GetAsync(id,true);

            artist.PersonalDetails.First = input.First;
            artist.PersonalDetails.Middle = input.Middle;
            artist.PersonalDetails.Last = input.Last;

            artist.PersonalDetails.DOB = input.DOB;

            await _artistRepository.UpdateAsync(artist);
        }

        [Route("Profile/{email}/{size}")]
        [HttpGet]
        public async Task<ArtistDto> GetArtistByEmail(string email, int size)
        {
            var artist = await GetArtistWithEmail(email);
            var toRet = ObjectMapper.Map<Artist, ArtistDto>(artist);
            toRet.Icon64 = ImageUtils.GetProfileIcon(artist.Id, size, Path.GetExtension(toRet.ImageFile));
            return toRet;
        }

        public async Task<UpdateArtistProfileDto> GetArtistProfileByEmail(string email)
        {
            var artist = await GetArtistWithEmail(email);
            var toRet = ObjectMapper.Map<Artist, UpdateArtistProfileDto>(artist);
            return toRet;
        }

        public async Task UpdateArtistProfileAsync(UpdateArtistProfileDto input)
        {
            var artist = await _artistRepository.GetAsync(input.ArtistId);
            if (artist == null)
            {
                throw new ArtistNotFoundException("By Id: " + input.ArtistId);
            }
            artist.Bio = input.Bio;
            artist.Statement = input.Statement;
            artist.ShortDesciption = input.ShortDesciption;
            await _artistRepository.UpdateAsync(artist);
        }

        [HttpPost]
        [Route("/Artist/Profile/FileUpload")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {

            var Request = _httpContextAccessor.HttpContext.Request;
            if (!RequestUtils.IsMultipartContentType(Request.ContentType))
            {

                //_logger.LogWarning($"The request content type [{Request.ContentType}] is invalid.");
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

                        //var trustedFilePath = Path.Combine(HonouredAppService.ImagesFolderPath, trustedFileNameForFileStorage);
                        //using (var targetStream = File.Create(trustedFilePath))
                        //{
                        //    await targetStream.WriteAsync(streamedFileContent);
                        //    formModel.TrustedFilePath = trustedFilePath;
                        //    formModel.TrustedFileName = trustedFileNameForDisplay;
                        //    //_logger.LogInformation($"Uploaded file '{trustedFileNameForDisplay}' saved to '{_targetFolderPath}' as {trustedFileNameForFileStorage}");
                        //}
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
                //_logger.LogInformation($"Uploaded file '{trustedFileNameForDisplay}' saved to '{_targetFolderPath}' as {trustedFileNameForFileStorage}");
            }
            // todo: validate and persist formModel
            //_logger.LogInformation(formModel.ToString());
            //MyFiles.Add(formModel);
            string iconString = ImageUtils.SaveArtistIconAndGetSmallString(IconSize.small, filePath, formModel.UserId, trustedFilePath);
                                                                            //Path.GetExtension(formModel.TrustedFileName));
           // var old = await GetAsync(formModel.ArtworkId);
            //var updt = ObjectMapper.Map<ArtistDto, UpdateArtistDto>(old);
            //updt.ImageFile = trustedFilePath;
            //updt.IsIconUploaded = true;
            //TODO copying dimensions
           // await UpdateAsync(formModel.ArtworkId, updt);
            return new OkObjectResult(iconString);
        }

        #endregion Implementations


        #region Private Methods
        private async Task<Artist> GetArtistWithEmail(string email)
        {
            var q = await _artistRepository.WithDetailsAsync(a => a.PersonalDetails);
            var artist = q.Where(a => a.PersonalDetails.Email.ToLower().Equals(email.ToLower()))
                            .FirstOrDefault(a => a.PersonalDetails.Email == email);
            if (artist == null)
            {
                throw new ArtistNotFoundException(email);
            }
            return artist;
        }

        public Task<ArtistDto> GetArtistByEmail(string email)
        {
            return GetArtistByEmail(email, (int) IconSize.small);
        }
        #endregion Private Methods
    }
}
