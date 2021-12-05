using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Honoured.Artists
{
    public class ArtistNotFoundException : BusinessException
    {
        #region Ctors
        public ArtistNotFoundException(string key) : base(HonouredDomainErrorCodes.ArtistAlreadyExistsException)
        {
            WithData("Id", key);
        }
        #endregion Ctors
    }
}
