using System;
using Volo.Abp;

namespace Honoured.ArtistSubscriptions
{
    public class InvalidArtistSubscriptionException : BusinessException
    {
        #region Ctors
        public InvalidArtistSubscriptionException(string key) :
            base(HonouredDomainErrorCodes.InvalidArtistSubscriptionException)
        {
            WithData("Message", key);
        }
        public InvalidArtistSubscriptionException(string key, Exception inner) :
            base(HonouredDomainErrorCodes.InvalidArtistSubscriptionException, innerException: inner)
        {
            WithData("Message", key);
        }
        #endregion Ctors
    }
}
