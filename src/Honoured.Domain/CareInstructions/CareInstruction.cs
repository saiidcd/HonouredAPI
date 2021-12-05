using Volo.Abp.Domain.Entities;

namespace Honoured.CareInstructions
{
    public class CareInstruction: Entity<long>
    {
        public string Instructions { get; set; }
    }
}
