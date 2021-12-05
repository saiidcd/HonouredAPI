using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.DTOs
{
    public class ContactPointDTO : EntityDto<long>
    {
        public ContactPointType Type { get; set; }

        public string Value { get; set; }

        public bool IsDefault { get; set; }
    }
}
