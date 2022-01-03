using Honoured.Dimensions;
using Honoured.Enumerations;
using System;
using Volo.Abp.Domain.Entities;

namespace Honoured.ArtistSubscriptions
{
    public class SubscriptionTier : Entity<long>
    {
        public string Name { get; set; }

        public int NumberOfPieces { get; set; }

        public double Price { get; set; }

        public GeneralStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public Dimension MaxDimension { get; set; }

        public long MaxDimensionId { get; set; }
    }
}
