using System;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace UserManagment.Users;

public class ExtendedUserAppService : UserManagmentAppService, IExtendedUserAppService
{
    private readonly ExtendedIdentityUserManager _userManager;
    private readonly IIdentityUserRepository _userRepository;

    public ExtendedUserAppService(
        ExtendedIdentityUserManager userManager,
        IIdentityUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<ExtendedUserDto> GetExtendedUserInfoAsync(string userId)
    {
        var user = await _userRepository.GetAsync(Guid.Parse(userId));

        return new ExtendedUserDto
        {
            LastLoginTime = user.GetLastLoginTime(),
            LoginAttemptCount = user.GetLoginAttemptCount(),
            UserStatus = user.GetUserStatus()
        };
    }

    public async Task SetUserStatusAsync(string userId, ExtendedUserStatus status)
    {
        var user = await _userRepository.GetAsync(Guid.Parse(userId));
        await _userManager.SetUserStatusAsync(user, status);
    }
}
