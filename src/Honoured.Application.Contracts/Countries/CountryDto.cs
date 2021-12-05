using Honoured.Enumerations;
using Honoured.Markets;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Countries
{
    public class CountryDto : EntityDto<long>
    {
        public string Name { get; set; }

        public GeneralStatus Status { get; set; }

        public List<MarketDto> Areas { get; set; }
    }
}
