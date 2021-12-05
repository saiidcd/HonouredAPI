using Honoured.Artists;
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
using System.Net.Http;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Honoured.ArtWorks
{
    [Route("Artwork")]
    [AllowAnonymous]
    public class ArtWorkAppService : CrudAppService<ArtWork,ArtWorkDto,long,
                                        GetArtWorkListDto,CreateArtWorkDto,UpdateArtWorkDto>
                                     
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IArtWorkRepository _artWorkRepository;
        private ArtistManager _artistManager;
        private IWebHostEnvironment _env;
        private Dictionary<long, string> _iconsMap = new Dictionary<long, string>();
        private string FileNotFoundBase64String;

        #region Ctors
        public ArtWorkAppService(IWebHostEnvironment env, IArtWorkRepository repo,  ArtistManager artistManager,
            IHttpContextAccessor httpContextAccessor) : base(repo)
        {
            GetPolicyName = HonouredPermissions.ArtWorks.Default;
            GetListPolicyName = HonouredPermissions.ArtWorks.Default;
            CreatePolicyName = HonouredPermissions.ArtWorks.Create;
            UpdatePolicyName = HonouredPermissions.ArtWorks.Update;
            DeletePolicyName = HonouredPermissions.ArtWorks.Delete;
            _httpContextAccessor = httpContextAccessor;
            _artWorkRepository = repo;
            _artistManager = artistManager;
            _env = env;
            FileNotFoundBase64String=ImageUtils.GetBase64ForImage(@"D:\test\Icons\Fnf.jpg");
        }
        #endregion Ctors

        #region Overrides
        [Route("Create")]
        public override async Task<ArtWorkDto> CreateAsync(CreateArtWorkDto input)
        {
            //var toAdd = ObjectMapper.Map<CreateArtWorkDto, ArtWork>(input);
            //toAdd.ArtistId = input.ArtistId;
            var t = await base.CreateAsync(input);
            return t;
            //var t = await _artWorkRepository.InsertAsync(toAdd);
            //var toRet = ObjectMapper.Map<ArtWork, ArtWorkDto>(toAdd);
            //return new ArtWorkDto();
            //return base.CreateAsync(input);
        }

        [Route("Upload")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {

            var Request = _httpContextAccessor.HttpContext.Request;
            if (!RequestUtils.IsMultipartContentType(Request.ContentType))
            {
                return new BadRequestResult();
            }

            var formModel = new FormData();

            var boundary = RequestUtils.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), new FormOptions().MultipartBoundaryLengthLimit);
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

            var filePath = ImageUtils.GetFilePath(formModel.UserId, formModel.ArtworkId);
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
            string iconString= ImageUtils.GenerateIconString(IconSize.small, filePath, formModel.TrustedFileName);
            var old = await GetAsync(formModel.ArtworkId);
            var updt = ObjectMapper.Map<ArtWorkDto, UpdateArtWorkDto>(old);
            updt.Status = ArtStatus.Uploaded;
            updt.StatusDate = DateTime.Now;
            updt.ImageFile = trustedFilePath;
            updt.IsFileUploaded = true;
            //TODO copying dimensions
            await UpdateAsync(formModel.ArtworkId, updt);
            return new OkObjectResult(iconString);
        }

        public async Task<string> Post()
        {
            try
            {
                var httpRequest = _httpContextAccessor.HttpContext.Request;
                if (httpRequest.Form.Files.Count > 0)
                {
                    foreach (var file in httpRequest.Form.Files)
                    {
                        string fileName = Path.Combine(@"d:\test\hon\", Path.GetRandomFileName());
                        using (var stream = File.Create(fileName ))
                        {
                            await file.CopyToAsync(stream);
                        }
                        return "/Uploads/" + fileName;
                    }
                }
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            return "no files";
        }
        [Route("test")]
        [AllowAnonymous]
        public async Task<JsonResult> Test()
        {
            Task<int> t = new Task<int>( () => 1);
            t.Start();
            var i = await t;
            return new JsonResult("worked!");
        }

        [HttpPost]
        [Route("ArtistPortfolio")]
        public async Task<PagedResultDto<ArtWorkDto>> GetPortfolioForArtist(GetArtWorkListDto input)
        {
            long.TryParse(input.Filter, out var artitstId);
            var artWorks = await _artWorkRepository.GetPortfolioByArtistId (
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                artitstId
            );

            var totalCount = artitstId <= 0
                ? await _artWorkRepository.CountAsync()
                : await _artWorkRepository.CountAsync(
                    a => a.ArtistId == artitstId);

            var dtos = ObjectMapper.Map<List<ArtWork>, List<ArtWorkDto>>(artWorks);
            var b64 = string.Empty;
            foreach (var art in dtos)
            {
                var artW = artWorks.First(i => i.Id == art.Id);
                if (!artW.IsFileUploaded)
                {
                    art.Icon64 = GetIconString(artW.Id);
                }
                else
                {
                    art.Icon64 = GetIconString(artW.ImageFile);
                }
            }

            return new PagedResultDto<ArtWorkDto>(
                            totalCount,
                            dtos
                        );
        }

        private string GetIconString(string fileName)
        {
            try
            {
                return ImageUtils.GetBase64ForImage(fileName);
            }
            catch (FileNotFoundException fne)
            {
                return FileNotFoundBase64String;
            }
        }
        private string GetIconString(long id)
        {

            var files = Directory.GetFiles(@"D:\test\Icons");

            var key = id % files.Length;
            if (_iconsMap.ContainsKey(key))
            {
                return _iconsMap[key];
            }

            var b64 = ImageUtils.GetBase64ForImage(files[key]);
            _iconsMap.Add(key, b64);
            return b64;
        }
        #endregion Overrides


        #region Implementations

        [HttpPost]
        [Route("FileUpload")]
        public async Task<UploadResult> FileUploadAsync(FileUploadDto fileData)
        {
            //var path = string.Empty;
            var file = fileData.Content;

            var uploadResult = new UploadResult();
            string trustedFileNameForFileStorage;
            var untrustedFileName = fileData.Filename;
            var artId = fileData.ArtWorkId;

            uploadResult.FileName = untrustedFileName;
            var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);
            try
            {
                //TODO get folder for storage
                trustedFileNameForFileStorage = Path.GetRandomFileName();
                var path = Path.Combine(_env.ContentRootPath,
                    _env.EnvironmentName, "unsafe_uploads",
                    trustedFileNameForFileStorage);

                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);

                //logger.LogInformation("{FileName} saved at {Path}",
                //    trustedFileNameForDisplay, path);
                uploadResult.Uploaded = true;
                uploadResult.StoredFileName = trustedFileNameForFileStorage;
            }
            catch (IOException ex)
            {
                //logger.LogError("{FileName} error on upload (Err: 3): {Message}",
                //    trustedFileNameForDisplay, ex.Message);
                uploadResult.ErrorCode = 3;
            }
            return uploadResult;
        }
        #endregion Implementations


    }
}
