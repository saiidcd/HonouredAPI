using Honoured.Enumerations;
using System;
using Volo.Abp.Domain.Entities;

namespace Honoured.Models
{
    public class Address : Entity<long>
    {

        #region Props
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

        #endregion Props

        #region Public Methods
        public override string ToString()
        {
            return ToString(";");
        }

        public string ToString(string sep)
        {
            return $"{Street1}{sep}{Street2}{sep}{City}{sep}{Province}{sep}{Country}";
        }
        #endregion Public Methods

    }
}
