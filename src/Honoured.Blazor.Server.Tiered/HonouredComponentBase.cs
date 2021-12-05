using Honoured.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Honoured.Blazor.Server.Tiered
{
    public abstract class HonouredComponentBase : AbpComponentBase
    {
        protected HonouredComponentBase()
        {
            LocalizationResource = typeof(HonouredResource);
        }
    }
}
