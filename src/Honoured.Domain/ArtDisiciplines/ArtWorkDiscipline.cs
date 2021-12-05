using Honoured.ArtWorks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honoured.ArtDisciplines
{
    public class ArtWorkDiscipline
    {
        public long ArtWorkId { get; set; }
        //[NotMapped]
        public ArtWork ArtWork { get; set; }

        public long DisciplineId { get; set; }

        //[NotMapped]
        public ArtDiscipline Discipline { get; set; }
    }
}
