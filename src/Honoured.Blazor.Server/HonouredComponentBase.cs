using Honoured.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Honoured.Blazor.Server
{
    public abstract class HonouredComponentBase : AbpComponentBase
    {
        protected HonouredComponentBase()
        {
            LocalizationResource = typeof(HonouredResource);
        }
    }
}
