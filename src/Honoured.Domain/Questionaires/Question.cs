using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Honoured.Questionaires
{
    public class Question : Entity<long>
    {
        public string Text { get; set; }

        public AnswerType TypeOfAnswer { get; set; }

        public List<QuestionAnswer> PossibleAnswers { get; set; }
    }
}
