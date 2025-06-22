using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using UserManagment.Localization;
using Volo.Abp.Domain;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace UserManagment;

[DependsOn(
    typeof(AbpIdentityDomainSharedModule),
    typeof(AbpValidationModule),
    typeof(AbpDddDomainSharedModule)
)]
public class UserManagmentDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<UserManagmentDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<UserManagmentResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/UserManagment");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("UserManagment", typeof(UserManagmentResource));
        });
    }
}
