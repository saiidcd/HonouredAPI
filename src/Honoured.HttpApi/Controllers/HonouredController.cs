using Honoured.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Honoured.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class HonouredController : AbpController
    {
        protected HonouredController()
        {
            LocalizationResource = typeof(HonouredResource);
        }
    }
}