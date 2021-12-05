using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Placements
{
    public class UpdatePlacementDto : EntityDto<long>
    {
        public long ArtWorkId { get; set; }

        public long ArtLoverId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public DateTime PickupDate { get; set; }

        public PlacementStatus Status { get; set; }

        public DateTime StatusDate { get; set; }
    }
}
