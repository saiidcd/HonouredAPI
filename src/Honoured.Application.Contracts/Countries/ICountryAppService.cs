using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Honoured.Countries
{
    public interface ICountryAppService : ICrudAppService<CountryDto, long, GetCountryListDto, CreateCountryDto,
                                                            UpdateCountryDto>
    {
    }
}
