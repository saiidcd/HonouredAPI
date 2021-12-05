using Honoured.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Honoured.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class HonouredPageModel : AbpPageModel
    {
        protected HonouredPageModel()
        {
            LocalizationResourceType = typeof(HonouredResource);
        }
    }
}