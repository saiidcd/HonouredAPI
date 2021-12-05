using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.DTOs
{
    public class AddressDTO : EntityDto<long>
    {
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public GeneralStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public AddressType Type { get; set; }

        public bool IsBilling { get; set; }

        public bool IsDeliveryPoint { get; set; }

        public bool IsDefault { get; set; }
    }
}
