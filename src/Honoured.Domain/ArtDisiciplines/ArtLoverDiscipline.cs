using Honoured.ArtLovers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honoured.ArtDisciplines
{
    public class ArtLoverDiscipline
    {
        public long ArtLoverId { get; set; }

        [NotMapped]
        public ArtLover ArtLover { get; set; }

        public long ArtDisciplineId { get; set; }

        [NotMapped]
        public ArtDiscipline ArtDiscipline { get; set; }
    }
}
