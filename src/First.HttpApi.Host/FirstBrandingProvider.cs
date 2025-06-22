using Microsoft.Extensions.Localization;
using First.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace First;

[Dependency(ReplaceServices = true)]
public class FirstBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<FirstResource> _localizer;

    public FirstBrandingProvider(IStringLocalizer<FirstResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
