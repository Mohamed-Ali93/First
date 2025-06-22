using Volo.Abp.Modularity;

namespace First;

public abstract class FirstApplicationTestBase<TStartupModule> : FirstTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
