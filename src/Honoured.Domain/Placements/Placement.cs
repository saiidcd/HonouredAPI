using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Placements
{
    public class Placement : Entity<long>
    {

        #region Props
        public long ArtWorkId { get; set; }

        public long ArtLoverId { get; set; }

        public long DimensionId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public DateTime PickupDate { get; set; }

        public PlacementStatus Status { get; set; }

        public DateTime StatusDate { get; set; }
        #endregion Props


        #region Methods
        public void Extend (TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        
        #endregion Methods
    }
}
