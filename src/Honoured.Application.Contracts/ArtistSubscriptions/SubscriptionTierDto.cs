using Volo.Abp.Application.Dtos;

namespace Honoured.ArtistSubscriptions
{
    public class SubscriptionTierDto : EntityDto<long>
    {

        #region Props

        public string Name { get; set; }

        public int NumbetOfPieces { get; set; }

        public double Price { get; set; }
        #endregion Props
    }
}
