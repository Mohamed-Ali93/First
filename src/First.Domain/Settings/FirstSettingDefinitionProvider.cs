using Volo.Abp.Settings;

namespace First.Settings;

public class FirstSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(FirstSettings.MySetting1));
    }
}
