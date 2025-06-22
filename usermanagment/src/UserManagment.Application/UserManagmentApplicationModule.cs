using Volo.Abp.Identity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace UserManagment;

[DependsOn(
    typeof(AbpIdentityApplicationModule),
    typeof(UserManagmentDomainModule),
    typeof(UserManagmentApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class UserManagmentApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<UserManagmentApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<UserManagmentApplicationModule>(validate: true);
        });
    }
}
