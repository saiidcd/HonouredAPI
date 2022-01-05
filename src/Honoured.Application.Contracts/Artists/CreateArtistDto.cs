using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.Artists
{
    public  class CreateArtistDto : EntityDto<long>
    {
        [Required]
        public string First { get; set; }

        public string Middle { get; set; }
        [Required]
        public string Last { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string Email { get; set; }

        public string ShortDesciption { get; set; }

        public string Bio { get; set; }

        public string Statement { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }
    }
}
