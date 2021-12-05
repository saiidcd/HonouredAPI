using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Honoured.Questionaires
{
    public class FilledQuestionaire : Entity<long>
    {
        public Questionaire QuestionaireUsed { get; set; }

        public Dictionary<Question, Answer> Answers { get; set; }
    }
}
