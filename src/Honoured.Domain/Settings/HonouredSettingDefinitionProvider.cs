using Volo.Abp.Settings;

namespace Honoured.Settings
{
    public class HonouredSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(HonouredSettings.MySetting1));
        }
    }
}
