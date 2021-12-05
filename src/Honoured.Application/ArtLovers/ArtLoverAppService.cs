using Honoured.ArtDisciplines;
using Honoured.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Honoured.ArtLovers
{
    [Authorize(HonouredPermissions.ArtLovers.Default)]
    [ExposeServices(typeof(ArtLoverAppService))]
    // [ExposeServices(typeof(IArtLoverAppService))]
    public class ArtLoverAppService : HonouredAppService, IArtLoverAppService, ITransientDependency
    {
        private IArtLoverRepository _ArtLoverRepository;

        public ArtLoverAppService(IArtLoverRepository repository) //: base(repository)
        {
            //GetPolicyName = HonouredPermissions.ArtLovers.Default;
            //GetListPolicyName = HonouredPermissions.ArtLovers.Default;
            //CreatePolicyName = HonouredPermissions.ArtLovers.Create;
            //UpdatePolicyName = HonouredPermissions.ArtLovers.Update;
            //DeletePolicyName = HonouredPermissions.ArtLovers.Delete;
            _ArtLoverRepository = repository;
        }


        #region Overrides
        [Authorize(HonouredPermissions.ArtLovers.Create)]
        public async Task<ArtLoverDto> CreateAsync(CreateArtLoverDto input)
        {
            //var artist = await _artistManager.CreateAsync(input.First, input.Middle, input.Last, input.DOB);
            var ArtLover = ObjectMapper.Map <CreateArtLoverDto, ArtLover>(input);
            ArtLover.Categories = GetCategories(input.Categories, ArtLover);
            await _ArtLoverRepository.InsertAsync(ArtLover);
            var toRet = ObjectMapper.Map<ArtLover, ArtLoverDto>(ArtLover);
            return toRet;
        }

        public async Task DeleteAsync(long id)
        {
            await _ArtLoverRepository.DeleteAsync(id);
        }

        public async Task<ArtLoverDto> GetAsync(long id)
        {
            var sub = await _ArtLoverRepository.GetAsync(id);
            var toRet = ObjectMapper.Map<ArtLover, ArtLoverDto>(sub);
            return toRet;
        }

        public async Task<PagedResultDto<ArtLoverDto>> GetListAsync(GetArtLoversListDto input)
        {
            var artists = await _ArtLoverRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );
            //var totalCount = 2;
            var totalCount = await _ArtLoverRepository.CountAsync(
                    author => (author.Status & input.Filter) == author.Status);

            return new PagedResultDto<ArtLoverDto>(
                totalCount,
                ObjectMapper.Map<List<ArtLover>, List<ArtLoverDto>>(artists)
            );
        }

        public async Task<ArtLoverDto> GetArtLoverByEmail(string email)
        {
            var q = await _ArtLoverRepository.GetQueryableAsync();
            var sub = q.FirstOrDefault(a => a.Profile.Email == email);
            if (sub == null)
            {
                throw new ArtLoverNotFoundException(email);
            }
            var toRet = ObjectMapper.Map<ArtLover, ArtLoverDto>(sub);
            return toRet;
        }

        public async Task UpdateAsync(long id, UpdateArtLoverDto input)
        {
            var sub = await _ArtLoverRepository.GetAsync(id, true);
            //var toUpdate = ObjectMapper.Map<UpdateArtLoverDto, ArtLover>(input);
            sub.Profile.First = input.First;
            sub.Profile.Middle = input.Middle;
            sub.Profile.Last = input.Last;

            sub.Profile.DOB = input.DOB;
            sub.Categories = GetCategories(input.Categories, sub);

            await _ArtLoverRepository.UpdateAsync(sub);
        }
        #endregion Overrides


        #region Private Methods
        private List<ArtLoverDiscipline> GetCategories(List<ArtDisciplineDTO> cats, ArtLover ArtLover)
        {
            var newCats = ObjectMapper.Map<List<ArtDisciplineDTO>, List<ArtDiscipline>>(cats);
            return newCats.Select(a => new ArtLoverDiscipline
                                    {
                                        ArtDiscipline = a,
                                        ArtLover = ArtLover,
                                        ArtLoverId = ArtLover.Id,
                                        ArtDisciplineId = a.Id
                                    }).ToList();
        }
        #endregion Private Methods
    }
}
