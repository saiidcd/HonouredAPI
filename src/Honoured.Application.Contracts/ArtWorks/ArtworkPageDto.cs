using Honoured.ArtDisciplines;
using Honoured.Dimensions;
using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Honoured.ArtWorks
{
    public class ArtworkPageDto
    {
        public List<ArtDisciplineDTO> Disciplines { get; set; }

        public List<DimensionDto> Dimensions { get; set; }

        public List<ArtStatus> AllowedStatuses { get; set; }
    }
}
