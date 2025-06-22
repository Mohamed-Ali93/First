using Volo.Abp.Identity;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace UserManagment;

[DependsOn(
    typeof(AbpIdentityDomainModule),
    typeof(AbpDddDomainModule),
    typeof(UserManagmentDomainSharedModule)
)]
public class UserManagmentDomainModule : AbpModule
{

}
