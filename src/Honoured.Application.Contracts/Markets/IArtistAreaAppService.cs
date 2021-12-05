using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Honoured.Markets
{
    public interface IArtistAreaAppService : ICrudAppService<MarketDto, long, GetMarketListDto,
                                                            CreateMarketDto, UpdateMarketDto>
    {
    }
}
