using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtistSubscriptions
{
    public class ArtistSubscriptionDto : EntityDto<long>
    {

        #region Props

        public long ArtitstId { get; set; }

        public DateTime StartDate { get; set; }

        public long TierId { get; set; }

        public ArtistSubscriptionsStatus Status { get; set; }

        public DateTime StatusDate { get; set; }
        #endregion Props
    }
}
