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
    }
}
