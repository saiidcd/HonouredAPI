using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Honoured.ArtLovers
{
    public class ArtLoverNotFoundException : BusinessException
    {
        #region Ctors
        public ArtLoverNotFoundException(string key) : base(HonouredDomainErrorCodes.ArtLoverNotFoundException)
        {
            WithData("Id", key);
        }
        #endregion Ctors
    }
}
