using Honoured.Artists;
using Honoured.ArtistSubscriptions;
using Honoured.Markets;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honoured.Facade
{
    [Route("SubsPortal")]
    public class AppSubscribe : HonouredAppService
    {

        #region Fields
        private ArtistAppService _artistService;
        private ArtistSubscriptionsAppService _subsService;
        private ArtistManager _artistManager;
        private ArtistSubsriptionManager _subsManager;
        private MarketsManager _marketsManager;
        #endregion Fields

        #region Ctor
        public AppSubscribe(ArtistAppService artistService, ArtistSubscriptionsAppService subsService,
            ArtistManager artistManager, ArtistSubsriptionManager subsManager, MarketsManager marketsManager)
        {
            _artistService = artistService;
            _subsService = subsService;
            _artistManager = artistManager;
            _subsManager = subsManager;
            _marketsManager = marketsManager;
        }
        #endregion Ctor


        #region Public Methods

        [Route("Subscriptions/Artist/PageInfo")]
        public async Task<ArtistSubscriptionPageDto> GetSubscrptionPageInfo()
        {
            var tiers = await _subsManager.GetActiveTiers();
            var markets = await _marketsManager.GetActive();
            var toRet = new ArtistSubscriptionPageDto
            {
                AvailableTiers = ObjectMapper.Map<List<SubscriptionTier>, List<SubscriptionTierDto>>(tiers),
                AvailablelMarkets = ObjectMapper.Map<List<Market>, List<MarketDto>>(markets)
            };
            return toRet;
        }

        [Route("/Artist")]
        public async Task<ArtistSubscriptionDto> NewArtistSubscriptionAsync(CreateArtistSubscriptionDto newSub)
        {
            ArtistDto artistDto = null;
            //create an artist if need be
            try
            {
                if (newSub.ArtitstId <= 0)
                {
                    artistDto = await _artistService.CreateAsync(newSub.ArtistInfo);
                }
                else
                {
                    artistDto = await _artistService.GetAsync(newSub.ArtitstId);
                }
            }
            catch (Exception e)
            {
                //TODO log this error
                throw new Exception($"Could not create/locate artist: {e.Message}", e);
            }
            newSub.ArtitstId = artistDto.Id;
            ValidateSubscription(newSub);

            //create a new subscription
            ArtistSubscriptionDto toRet = null;
            try
            {
                return await _subsService.CreateAsync(newSub);
            }
            catch (Exception e)
            {
                //TODO log this error
                throw new InvalidArtistSubscriptionException($"Error while attempting to create subscription: {e.Message}", e);
            }
        }

        #endregion Public Methods


        #region Private Methods
        private void ValidateSubscription(CreateArtistSubscriptionDto newSub)
        {
            if(newSub.TierId <=0)
            {
                throw new InvalidArtistSubscriptionException($"Missing Subscription Tier info!");

            }

            if (newSub.AreaIds == null || !newSub.AreaIds.Any())
            {
                throw new InvalidArtistSubscriptionException($"You need to sepcify at least one area!");

            }
        }
        #endregion Private Methods
    }
}
