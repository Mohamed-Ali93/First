using Volo.Abp.Identity;
using Localization.Resources.AbpUi;
using UserManagment.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagment;

[DependsOn(
    typeof(AbpIdentityHttpApiModule),
    typeof(UserManagmentApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class UserManagmentHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(UserManagmentHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<UserManagmentResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
