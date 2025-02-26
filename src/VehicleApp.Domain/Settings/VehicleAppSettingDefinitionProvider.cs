using Volo.Abp.Settings;

namespace VehicleApp.Domain.Settings;

public class VehicleAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(VehicleAppSettings.MySetting1));
    }
}
