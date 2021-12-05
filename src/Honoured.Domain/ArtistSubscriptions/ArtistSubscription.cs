using Honoured.Enumerations;
using Honoured.Markets;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Honoured.ArtistSubscriptions
{
    public class ArtistSubscription : Entity<long>
    {
        public long ArtitstId { get; set; }

        public DateTime StartDate { get; set; }

        public SubscriptionTier Tier { get; set; }

        public GeneralStatus Status { get; set; }

        public DateTime StatusDate { get; set; }
    }
}
