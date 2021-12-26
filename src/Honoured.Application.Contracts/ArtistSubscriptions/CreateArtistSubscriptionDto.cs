using Honoured.Artists;
using System;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtistSubscriptions
{
    public class CreateArtistSubscriptionDto : EntityDto<long>
    {

        #region Props
        public long ArtitstId { get; set; }

        public DateTime StartDate { get; set; }

        public long TierId { get; set; }

        public CreateArtistDto ArtistInfo { get; set;

        public List<long> AreaIds { get; set; }
        #endregion Props
    }
}
