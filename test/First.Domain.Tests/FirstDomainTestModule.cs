using Volo.Abp.Modularity;

namespace First;

[DependsOn(
    typeof(FirstDomainModule),
    typeof(FirstTestBaseModule)
)]
public class FirstDomainTestModule : AbpModule
{

}
