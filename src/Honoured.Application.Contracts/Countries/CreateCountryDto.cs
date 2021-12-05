using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Countries
{
    public class CreateCountryDto : EntityDto<long>
    {
        public string Name { get; set; }

        public GeneralStatus Status { get; set; }
    }
}
