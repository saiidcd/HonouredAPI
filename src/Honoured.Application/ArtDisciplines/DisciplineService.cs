using Honoured.Permissions;
using System.Collections.Generic;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Honoured.ArtDisciplines
{
    //[ExposeServices(typeof(IDisciplineSvc))]
    public class DisciplineService : CrudAppService<ArtDiscipline, ArtDisciplineDTO, long, GetDisciplineListDto,
                        ArtDisciplineDTO, ArtDisciplineDTO>
    {

        #region Ctors
        public DisciplineService(IRepository<ArtDiscipline, long> repository) : base(repository)
        {
            GetPolicyName = HonouredPermissions.Disciplines.Default;
            GetListPolicyName = HonouredPermissions.Disciplines.Default;
            CreatePolicyName = HonouredPermissions.Disciplines.Create;
            UpdatePolicyName = HonouredPermissions.Disciplines.Update;
            DeletePolicyName = HonouredPermissions.Disciplines.Delete;
        }
        #endregion Ctors


        #region Public Mehtods
        public List<ArtDisciplineDTO> GetAllActive()
        {
            var models = Repository.GetListAsync(d => d.Status == Enumerations.GeneralStatus.Active).GetAwaiter().GetResult();
            return ObjectMapper.Map<List<ArtDiscipline>, List<ArtDisciplineDTO>>(models);
        }
        #endregion Public Mehtods
    }
}
