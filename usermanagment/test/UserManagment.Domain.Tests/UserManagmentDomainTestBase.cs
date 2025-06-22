using Volo.Abp.Modularity;

namespace UserManagment;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class UserManagmentDomainTestBase<TStartupModule> : UserManagmentTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
