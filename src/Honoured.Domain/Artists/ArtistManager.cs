using Honoured.ArtistSubscriptions;
using Honoured.Markets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Honoured.Artists
{
    public class ArtistManager : DomainService
    {

        #region Fields
        private readonly IArtistRepository _artistRepository;
        private readonly MarketsManager _marketsManager;
        #endregion Fields
        #region Ctors
        public ArtistManager(IArtistRepository repo, MarketsManager marketsManager)
        {
            _artistRepository = repo;
            _marketsManager = marketsManager;
        }
        #endregion Ctors


        #region Public Methods
        public async Task<Artist> CreateAsync(string first, string middle, string last, DateTime dob)
        {
            Check.NotNullOrWhiteSpace(first, nameof(first));
            Check.NotNullOrWhiteSpace(middle, nameof(middle));
            Check.NotNullOrWhiteSpace(last, nameof(last));
            Check.NotNull(dob, nameof(dob));

            var existingArtist = await _artistRepository.FindArtisByKeyAsync(first, middle, last, dob);
            if (existingArtist != null)
            {
                throw new ArtistAlreadyExistsException($"{first} {middle} {last} with Date of birth: {dob}");
            }
            return new Artist(first, middle, last, dob);
        }
        public Artist CreateDummy(long id)
        {
            return new Artist(id);
        }

        public async Task<bool> UpdateAreas(long artistId, List<long> areaIds)
        {
            var existingArtist = await _artistRepository.GetAsync(artistId);
            if(existingArtist == null)
            {
                throw new ArtistNotFoundException(artistId.ToString());
            }
            var markets = await _marketsManager.GetMarketsByIds(areaIds);
            existingArtist.SubscribedMarkets = markets;
            await _artistRepository.UpdateAsync(existingArtist);
            return true;
        }
        #endregion Public Methods
    }
}
