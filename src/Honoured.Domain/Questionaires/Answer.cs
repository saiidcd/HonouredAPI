using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Questionaires
{
    public class Answer : Entity<long>
    {
        public AnswerType MyProperty { get; set; }

        public string Text { get; set; }
    }
}
