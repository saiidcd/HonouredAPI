using Honoured.DTOs;
using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Deliveries
{
    public class CreateDeliveryDto : EntityDto<long>
    {
        public long ArtWorkId { get; set; }

        public long ArtLoverId { get; set; }

        public AddressDTO Address { get; set; }

        public DeliveryStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public DeliveryType Type { get; set; }
    }
}
