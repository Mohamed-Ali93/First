using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace UserManagment;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class UserManagmentInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<UserManagmentInstallerModule>();
        });
    }
}
