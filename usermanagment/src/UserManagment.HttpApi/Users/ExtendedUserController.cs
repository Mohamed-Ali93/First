using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace UserManagment.Users;

[Area(UserManagmentRemoteServiceConsts.ModuleName)]
[RemoteService(Name = UserManagmentRemoteServiceConsts.RemoteServiceName)]
[Route("api/user-managment/users/extended")]
public class ExtendedUserController : UserManagmentController, IExtendedUserAppService
{
    private readonly IExtendedUserAppService _extendedUserAppService;

    public ExtendedUserController(IExtendedUserAppService extendedUserAppService)
    {
        _extendedUserAppService = extendedUserAppService;
    }

    [HttpGet]
    [Route("{userId}")]
    [Authorize]
    public Task<ExtendedUserDto> GetExtendedUserInfoAsync(string userId)
    {
        return _extendedUserAppService.GetExtendedUserInfoAsync(userId);
    }

    [HttpPost]
    [Route("{userId}/status")]
    [Authorize]
    public Task SetUserStatusAsync(string userId, ExtendedUserStatus status)
    {
        return _extendedUserAppService.SetUserStatusAsync(userId, status);
    }
}
