namespace Honoured
{
    public static class HonouredDomainErrorCodes
    {
        /* You can add your business exception error codes here, as constants */

        #region Artists 00100XX
        public const string ArtistAlreadyExistsException = "Honoured:0010001";
        #endregion Artists

        #region ArtLovers 00101XX
        public const string ArtLoverNotFoundException = "Honoured:0010101";
        #endregion ArtLovers

        #region ArtistSubs 00102XX
        public const string InvalidArtistSubscriptionException = "Honoured:0010201";
        #endregion ArtistSubs
    }
}
