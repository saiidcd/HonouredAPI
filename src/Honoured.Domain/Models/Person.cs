using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Models
{
    public class Person : Entity<long>
    {

        #region Props
        public long ParentId { get; set; }

        public PersonType ParentType { get; set; }

        public string First { get; set; }

        public string Middle { get; set; }

        public string Last { get; set; }

        public DateTime DOB { get; set; }

        public string Email { get; set; }

        public List<Address> Addresses { get; set; } = new List<Address>();

        public List<ContactPoint> ContactPoints { get; set; } = new List<ContactPoint>();

        [NotMapped]
        public Address Address { get { return Addresses.FirstOrDefault(a => a.IsDefault); } }

        [NotMapped]
        public Address BillingAddress { get { return Addresses.FirstOrDefault(a => a.IsBilling); } }

        [NotMapped]
        public List<Address> DeliveryPoints { get { return Addresses.Where(a => a.IsDeliveryPoint).ToList(); } }
        #endregion Props


        #region Methods
        public ContactPoint GetPhone()
        {
            var toRet = ContactPoints.FirstOrDefault(cp => cp.Type == ContactPointType.Mobile);
            return toRet ?? ContactPoints.FirstOrDefault(cp => cp.Type == ContactPointType.Landline);
        }
        public string GetFullName()
        {
            return $"{First} {Middle} {Last}".Replace("  ", " ");
        }

        #endregion Methods


    }
}
