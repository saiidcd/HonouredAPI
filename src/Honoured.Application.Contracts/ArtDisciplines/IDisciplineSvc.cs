using Volo.Abp.Application.Services;

namespace Honoured.ArtDisciplines
{
    public interface IDisciplineSvc : ICrudAppService< //Defines CRUD methods
        ArtDisciplineDTO, //Used to show
        long, //Primary key of the book entity
        GetDisciplineListDto, //Used for paging/sorting on getting a list
        ArtDisciplineDTO, //Used to create a new instance
        ArtDisciplineDTO> //Used to update an instance
    {
    }
}
