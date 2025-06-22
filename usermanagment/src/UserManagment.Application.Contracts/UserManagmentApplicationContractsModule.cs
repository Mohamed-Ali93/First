using Volo.Abp.Identity;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace UserManagment;

[DependsOn(
    typeof(AbpIdentityApplicationContractsModule),
    typeof(UserManagmentDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class UserManagmentApplicationContractsModule : AbpModule
{

}
