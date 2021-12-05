using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Questionaires
{
    public class Questionaire : Entity<long>
    {
        public string Name { get; set; }

        public Question FirstQuestion { get; set; }
    }
}
