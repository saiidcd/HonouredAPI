using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Questionaires
{
    public class QuestionAnswer : Entity<long>
    {
        public string Text { get; set; }

        public Question? NextQuestion { get; set; }

    }
}
