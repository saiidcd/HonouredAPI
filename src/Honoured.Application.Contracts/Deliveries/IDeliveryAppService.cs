using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Honoured.Deliveries
{
    public interface IDeliveryAppService : ICrudAppService<DeliveryDto, long, GetDeliveryListDto,
                                                                CreateDeliveryDto, UpdateDeliveryDto>
    {
    }
}
