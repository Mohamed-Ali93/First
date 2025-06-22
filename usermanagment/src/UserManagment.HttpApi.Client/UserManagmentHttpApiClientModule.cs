using Volo.Abp.Identity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace UserManagment;

[DependsOn(
    typeof(AbpIdentityHttpApiClientModule),
    typeof(UserManagmentApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class UserManagmentHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(UserManagmentApplicationContractsModule).Assembly,
            UserManagmentRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<UserManagmentHttpApiClientModule>();
        });

    }
}
