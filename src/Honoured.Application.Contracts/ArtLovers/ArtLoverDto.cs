using Honoured.DTOs;
using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtLovers
{
    public class ArtLoverDto : EntityDto<long>
    {
        public string Name { 
            get => $"{First} {Middle} {Last}"; }
        public string First { get; set; }

        public string Middle { get; set; }

        public string Last { get; set; }

        public DateTime DOB { get; set; }

        public string Email { get; set; }

        public long ArtLoverId { get; set; }

        public ArtLoverStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public int DesiredPlacements { get; set; }

        public int ActivePlacements { get; set; }

        public List<AddressDTO> Addresses { get; set; } = new List<AddressDTO>();

        public List<ContactPointDTO> ContactPoints { get; set; } = new List<ContactPointDTO>();
    }
}
