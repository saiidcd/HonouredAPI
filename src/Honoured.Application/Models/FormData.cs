using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honoured.Models
{
    public class FormData
    {
        public string TrustedFilePath { get; set; }
        public string TrustedFileName { get; set; }
        public long UserId { get; set; }
        public string Comment { get; set; }
        public string Guid { get; private set; } = System.Guid.NewGuid().ToString();
        public bool IsPrimary { get; set; }
        public long ArtworkId { get; internal set; }
        public string Email { get; internal set; }

        public override string ToString()
        {
            return $"{nameof(TrustedFilePath)}: [{TrustedFilePath}];" + Environment.NewLine +
                   $"{nameof(UserId)}: {UserId}; " + Environment.NewLine +
                   $"{nameof(Guid)}: {Guid}; " + Environment.NewLine +
                   $"{nameof(IsPrimary)}: {IsPrimary}; ";
        }
    }
}
