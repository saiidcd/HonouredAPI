using Honoured.Enumerations;
using Honoured.Markets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Countries
{
    public class Country :Entity<long>
    {
        public string Name { get; set; }

        public GeneralStatus Status { get; set; }

        public List<Market> Areas { get; set; }
    }
}
