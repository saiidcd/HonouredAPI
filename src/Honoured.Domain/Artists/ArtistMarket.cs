using Honoured.Enumerations;
using Honoured.Markets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Artists
{
    public class ArtistMarket : Entity<long>
    {
        public long Artistid { get; set; }

        public long MarketId { get; set; }


        public Artist Artist { get; set; }

        public Market Market { get; set; }

        public GeneralStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
