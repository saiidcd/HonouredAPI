using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Honoured.Artists
{
    public class ArtistAlreadyExistsException : BusinessException
    {

        #region Ctors
        public ArtistAlreadyExistsException(string key) : base(HonouredDomainErrorCodes.ArtistAlreadyExistsException)
        {
            WithData("Name and dob", key);
        }
        #endregion Ctors
    }
}
