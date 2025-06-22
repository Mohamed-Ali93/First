using Volo.Abp.Modularity;

namespace First;

/* Inherit from this class for your domain layer tests. */
public abstract class FirstDomainTestBase<TStartupModule> : FirstTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
