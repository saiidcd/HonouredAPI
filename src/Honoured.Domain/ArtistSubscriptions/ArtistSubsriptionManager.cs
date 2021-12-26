using Honoured.Artists;
using Honoured.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Honoured.ArtistSubscriptions
{
    public class ArtistSubsriptionManager : DomainService
    {
        #region Fields
        private IArtistSubscriptionRepository _repo;
        private ArtistManager _artistManager;
        #endregion Fields


        #region Ctors
        public ArtistSubsriptionManager(IArtistSubscriptionRepository repo, ArtistManager artistManager)
        {
            _repo = repo;
            _artistManager = artistManager;
        }
        #endregion Ctors


        #region Public methods
        public async Task<ArtistSubscription> Create(long artistId, SubscriptionTier tier)
                => await Create(artistId, tier, DateTime.Now);
        public async Task<ArtistSubscription> Create(long artistId, SubscriptionTier tier, DateTime startingDate)
        {
            var toRet = new ArtistSubscription { 
                ArtitstId = artistId,
                StartDate = startingDate,
                Tier = tier,
                Status = ArtistSubscriptionsStatus.Requested,
                StatusDate = DateTime.Now
            };
            await _repo.InsertAsync(toRet);
            return toRet;
        }
        #endregion Public methods
    }
}
