using Volo.Abp.Modularity;

namespace First;

[DependsOn(
    typeof(FirstApplicationModule),
    typeof(FirstDomainTestModule)
)]
public class FirstApplicationTestModule : AbpModule
{

}
