using Honoured.Dimensions;
using Honoured.Enumerations;
using Honoured.Models;
using Honoured.Placements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Subscriptions
{
    public class Subscription : Entity<long>
    {

        #region Props
        public long ArtLoverId { get; set; }

        public int NumberOfPieces { get; set; }

        public Dimension Dimension { get; set; }

        public Address DeliveryAddress { get; set; }

        public SubscriptionStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public List<Placement> CurrentPlacements { get; set; }

        public DateTime NextDeliveryDate { get; set; }

        public bool IsOneArtistPerPlacement { get; set; }

        #endregion Props


        #region Methods

        #region Placement Operations
        public Placement AddPlacement(long dimensionId)
        {
            throw new NotImplementedException();
        }

        
        #endregion Placement Operations
        #endregion Methods
    }
}
