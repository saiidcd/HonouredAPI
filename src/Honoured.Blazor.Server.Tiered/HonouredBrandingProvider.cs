using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Honoured.Blazor.Server.Tiered
{
    [Dependency(ReplaceServices = true)]
    public class HonouredBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Honoured";
    }
}
