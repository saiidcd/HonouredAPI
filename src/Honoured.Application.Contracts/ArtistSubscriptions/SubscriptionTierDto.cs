using Honoured.Dimensions;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtistSubscriptions
{
    public class SubscriptionTierDto : EntityDto<long>
    {

        #region Props

        public string Name { get; set; }

        public int NumberOfPieces { get; set; }

        public double Price { get; set; }

        public DimensionDto MaxDimension { get; set; }
        #endregion Props
    }
}
