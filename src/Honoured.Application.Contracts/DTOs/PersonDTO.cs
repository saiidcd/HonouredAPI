using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.DTOs
{
    public class PersonDTO : EntityDto<long>
    {

        #region Props
        public string First { get; set; }

        public string Middle { get; set; }

        public string Last { get; set; }

        public DateTime DOB { get; set; }

        public List<AddressDTO> Addresses { get; set; }

        public List<ContactPointDTO> ContactPoints { get; set; }
        #endregion Props

    }
}
