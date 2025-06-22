using First.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace First.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(FirstEntityFrameworkCoreModule),
    typeof(FirstApplicationContractsModule)
)]
public class FirstDbMigratorModule : AbpModule
{
}
