using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Threading;
using Volo.Abp.Uow;
using Volo.Abp.Security.Claims;

namespace UserManagment.Users;

public class ExtendedIdentityUserManager : IdentityUserManager, ITransientDependency
{
    public ExtendedIdentityUserManager(
        IdentityUserStore store,
        IIdentityRoleRepository roleRepository,
        IIdentityUserRepository userRepository,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<IdentityUser> passwordHasher,
        IEnumerable<IUserValidator<IdentityUser>> userValidators,
        IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<IdentityUserManager> logger,
        ICancellationTokenProvider cancellationTokenProvider,
        IOrganizationUnitRepository organizationUnitRepository,
        ISettingProvider settingProvider,
        IDistributedEventBus distributedEventBus,
        IIdentityLinkUserRepository linkUserRepository,
        IDistributedCache<AbpDynamicClaimCacheItem> dynamicClaimCache
    ) : base(
        store,
        roleRepository,
        userRepository,
        optionsAccessor,
        passwordHasher,
        userValidators,
        passwordValidators,
        keyNormalizer,
        errors,
        services,
        logger,
        cancellationTokenProvider,
        organizationUnitRepository,
        settingProvider,
        distributedEventBus,
        linkUserRepository,
        dynamicClaimCache)
    {
    }

    public virtual async Task UpdateLastLoginTimeAsync(IdentityUser user)
    {
        user.SetLastLoginTime(DateTime.UtcNow);
        await UpdateAsync(user);
    }

    public virtual async Task IncrementLoginAttemptAsync(IdentityUser user)
    {
        var currentCount = user.GetLoginAttemptCount();
        user.SetLoginAttemptCount(currentCount + 1);
        await UpdateAsync(user);
    }

    public virtual async Task ResetLoginAttemptsAsync(IdentityUser user)
    {
        user.SetLoginAttemptCount(0);
        await UpdateAsync(user);
    }

    public virtual async Task<IdentityResult> SetUserStatusAsync(IdentityUser user, ExtendedUserStatus status)
    {
        user.SetUserStatus(status);
        return await UpdateAsync(user);
    }
}
