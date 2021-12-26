﻿using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtistSubscriptions
{
    public class UpdateArtistSubscriptionDto : EntityDto<long>
    {

        #region Props
        public long SubscriptionId { get; set; }

        public long ArtistId { get; set; }

        public ArtistSubscriptionsStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public long TierId { get; set; }
        #endregion Props
    }
}
