using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Honoured.Web
{
    [Dependency(ReplaceServices = true)]
    public class HonouredBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Honoured";
    }
}
