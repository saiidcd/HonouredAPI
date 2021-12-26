using Honoured.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Honoured.ArtistSubscriptions
{
    [Route("Subscriptions/Artist")]
    [Authorize(HonouredPermissions.ArtistSubscriptions.Default)]
    public class ArtistSubscriptionsAppService : HonouredAppService, IArtistSubscriptionService
    {

        #region Fields
        private IArtistSubscriptionRepository _substRepository;
        private ArtistSubsriptionManager _subsManager;
        private IWebHostEnvironment _env;
        private IHttpContextAccessor _httpContextAccessor;

        #endregion Fields

        #region Ctors
        public ArtistSubscriptionsAppService(IWebHostEnvironment env, IArtistSubscriptionRepository repo, 
                                                ArtistSubsriptionManager mgr,
                                                IHttpContextAccessor httpContextAccessor)
        {
            _substRepository = repo;
            _subsManager = mgr;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion Ctors
        #region Implementation
        public Task<ArtistSubscriptionDto> CreateAsync(CreateArtistSubscriptionDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ArtistSubscriptionDto> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<ArtistSubscriptionDto>> GetListAsync(GetArtistSubscriptionListDto input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(long id, UpdateArtistSubscriptionDto input)
        {
            throw new NotImplementedException();
        }
        #endregion Implementation
    }
}
