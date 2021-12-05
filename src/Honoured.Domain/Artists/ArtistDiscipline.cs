using Honoured.ArtDisciplines;
using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Artists
{
    public class ArtistDiscipline : Entity<long>
    {
        public long ArtistId { get; set; }

        public long DisciplineId { get; set; }

        public Artist Artist { get; set; }

        public ArtDiscipline Discipline { get; set; }

        public DateTime AddedOn { get; set; }

        public GeneralStatus Status { get; set; }

        public DateTime StatusDate { get; set; }
    }
}
