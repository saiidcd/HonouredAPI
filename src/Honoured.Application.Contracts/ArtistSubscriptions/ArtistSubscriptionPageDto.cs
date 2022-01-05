using Honoured.Markets;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtistSubscriptions
{
    public class ArtistSubscriptionPageDto : EntityDto<long>
    {

        #region Props
        public List<MarketDto> AvailablelMarkets { get; set; }

        public List<SubscriptionTierDto> AvailableTiers { get; set; }
        #endregion Props
    }
}
