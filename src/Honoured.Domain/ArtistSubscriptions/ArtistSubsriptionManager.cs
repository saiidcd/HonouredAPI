using Honoured.Artists;
using Honoured.Enumerations;
using Honoured.Markets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Honoured.ArtistSubscriptions
{
    public class ArtistSubsriptionManager : DomainService
    {
        #region Fields
        private IArtistSubscriptionRepository _repo;
        private ArtistManager _artistManager;
        private MarketsManager _marketsManager;
        private IRepository<SubscriptionTier, long> _tierRepo;
        private ISubscriptionTiersRepository _tiersRepository;
        #endregion Fields


        #region Ctors
        public ArtistSubsriptionManager(IArtistSubscriptionRepository repo,
                                        IRepository<SubscriptionTier, long> tierRepo,
                                        ISubscriptionTiersRepository subscriptionTiers,
                                        ArtistManager artistManager,
                                        MarketsManager marketsManager)
        {
            _repo = repo;
            _artistManager = artistManager;
            _marketsManager = marketsManager;
            _tierRepo = tierRepo;
            _tiersRepository = subscriptionTiers;
        }
        #endregion Ctors


        #region Public methods
        public async Task<ArtistSubscription> Create(long artistId, long tierId, List<long> marketIds)
                => await Create(artistId, tierId, marketIds, DateTime.Now);

        public async Task<List<SubscriptionTier>> GetActiveTiers()
        {
            return await _tiersRepository.GetTiers(true);
            //return await _tierRepo.GetListAsync(t=>t.Status== GeneralStatus.Active,true);
        }

        public async Task<ArtistSubscription> Create(long artistId, long tierId,
                                                        List<long> marketIds, DateTime startingDate)
        {
            var tier = await GetTierById(tierId);
            var toRet = new ArtistSubscription { 
                ArtitstId = artistId,
                StartDate = startingDate,
                Tier = tier,
                Status = ArtistSubscriptionsStatus.Requested,
                StatusDate = DateTime.Now
            };
            await _repo.InsertAsync(toRet);
            await _artistManager.UpdateAreas(artistId, marketIds);
            return toRet;
        }
        #endregion Public methods


        #region Private methods
        private async Task<SubscriptionTier> GetTierById(long tierId)
        {
            return await _tierRepo.GetAsync(tierId);
        }
        #endregion Private methods
    }
}
