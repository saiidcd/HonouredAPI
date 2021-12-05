using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Models
{
    public class ContactPoint : Entity<long>
    {
        public ContactPointType Type { get; set; }

        public string Value { get; set; }

        public bool IsDefault { get; set; }
    }
}
