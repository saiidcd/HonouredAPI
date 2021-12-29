using Honoured.Artists;
using Honoured.ArtistSubscriptions;
using Honoured.ArtLovers;
using Honoured.DTOs;
using Honoured.Markets;
using Microsoft.AspNetCore.Authorization;
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
        private ArtLoverAppService _artLoverService;
        #endregion Fields

        #region Ctor
        public AppSubscribe(ArtistAppService artistService, ArtistSubscriptionsAppService subsService,
            ArtistManager artistManager, ArtistSubsriptionManager subsManager, MarketsManager marketsManager,
            ArtLoverAppService artLoverService)
        {
            _artistService = artistService;
            _subsService = subsService;
            _artistManager = artistManager;
            _subsManager = subsManager;
            _marketsManager = marketsManager;
            _artLoverService = artLoverService;
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

        [AllowAnonymous]
        [Route("Subscriptions")]
        [HttpPost]
        public async Task<SubscriberDto> Register(SubscriberDto subscriber)
        {
            long newId = -1L;
            if (subscriber.IsArtist)
            {
                var newartist = ObjectMapper.Map<SubscriberDto, CreateArtistDto>(subscriber);
                var artistDto = await _artistService.CreateAsync(newartist);
                newId = artistDto.Id;
            }

            if (subscriber.IsArtLover)
            {
                var newartlover = ObjectMapper.Map<SubscriberDto, CreateArtLoverDto>(subscriber);
                var artLoverDto = await _artLoverService.CreateAsync(newartlover);
                newId = newId <= 0 ? artLoverDto.Id : newId;
            }
            subscriber.Id = newId;
            subscriber.ArtistId = newId;
            return subscriber;
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
