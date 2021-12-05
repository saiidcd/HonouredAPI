using Honoured.Artists;
using Honoured.ArtWorks;
using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Honoured.ArtDisciplines
{
    public class ArtDiscipline : Entity<long>
    {
        public string Name { get; set; }

        public TypeOfArt ArtType { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public GeneralStatus Status { get; set; }

        public DateTime StatusDate { get; set; }

        public List<Artist> Artists { get; set; }

        public List<ArtWork> ArtWorks { get; set; }
    }
}
