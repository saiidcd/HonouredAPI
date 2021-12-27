using Honoured.ArtistSubscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Honoured.Markets
{
    public class MarketsManager : DomainService
    {

        #region Fields
        private IRepository<Market, long> _repo;
        #endregion Fields

        #region Ctors
        public MarketsManager(IRepository<Market, long> repo)
        {
            _repo = repo;
        }
        #endregion Ctors


        #region Public Methods
        public Task<List<Market>> GetActive() => GetAll(true);

        public Task<List<Market>> GetAll(bool isActiveOnly = true)
        {
            if (isActiveOnly)
            {
                return _repo.GetListAsync(m => m.Status == Enumerations.GeneralStatus.Active);
            }
            return _repo.GetListAsync();
        }

        public async Task<List<Market>> GetMarketsByIds(List<long> areaIds)
        {
            var markets = await _repo.GetListAsync(i => i.Id.IsIn(areaIds));
            if (markets.Count!=areaIds.Count)
            {
                throw new InvalidArtistSubscriptionException("Area ids contain one or more invalid entries!");
            }
            return markets;
        }
        #endregion Public Methods
    }
}
