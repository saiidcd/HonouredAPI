using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Honoured.ArtDisciplines
{
    public class ArtDisciplineDTO : EntityDto<long>
    {
        public string Name { get; set; }

        public TypeOfArt ArtType { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public GeneralStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public List<ArtDisciplineDTO> Categories { get; set; }
    }
}
