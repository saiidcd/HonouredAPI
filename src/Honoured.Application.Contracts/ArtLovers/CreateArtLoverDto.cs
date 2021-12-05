using Honoured.ArtDisciplines;
using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtLovers
{
    public class CreateArtLoverDto :EntityDto<long>
    {
        public string First { get; set; }

        public string Middle { get; set; }

        public string Last { get; set; }

        public DateTime DOB { get; set; }

        public ArtLoverStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public int DesiredPlacements { get; set; }

        public string Email { get; set; }

        public List<ArtDisciplineDTO> Categories { get; set; }
    }
}
