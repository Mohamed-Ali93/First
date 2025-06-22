using Volo.Abp.Modularity;

namespace UserManagment;

[DependsOn(
    typeof(UserManagmentApplicationModule),
    typeof(UserManagmentDomainTestModule)
    )]
public class UserManagmentApplicationTestModule : AbpModule
{

}
