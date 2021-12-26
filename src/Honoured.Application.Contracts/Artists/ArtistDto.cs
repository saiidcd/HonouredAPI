using Honoured.DTOs;
using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Artists
{
    public class ArtistDto : EntityDto<long>
    {
        //public PersonDTO PersonalDetails { get; set; }
        //public string Name { get; set; }

        public string First { get; set; }

        public string Middle { get; set; }

        public string Last { get; set; }

        public DateTime DOB { get; set; }

        public long ArtistId { get; set; }

        public ArtistStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public string ShortDesciption { get; set; }

        public string Bio { get; set; }

        public string Statement { get; set; }

        public string Email { get; set; }

        public string ImageFile { get; set; }

        public bool IsIconUploaded { get; set; }
        public string Icon64 { get; set; }

        public List<AddressDTO> Addresses { get; set; } = new List<AddressDTO>();

        public List<ContactPointDTO> ContactPoints { get; set; } = new List<ContactPointDTO>();
    }
}
