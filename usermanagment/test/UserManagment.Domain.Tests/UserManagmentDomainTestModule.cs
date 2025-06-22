using Volo.Abp.Modularity;

namespace UserManagment;

[DependsOn(
    typeof(UserManagmentDomainModule),
    typeof(UserManagmentTestBaseModule)
)]
public class UserManagmentDomainTestModule : AbpModule
{

}
