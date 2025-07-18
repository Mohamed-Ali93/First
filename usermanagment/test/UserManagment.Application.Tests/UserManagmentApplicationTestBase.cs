﻿using Volo.Abp.Modularity;

namespace UserManagment;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class UserManagmentApplicationTestBase<TStartupModule> : UserManagmentTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
