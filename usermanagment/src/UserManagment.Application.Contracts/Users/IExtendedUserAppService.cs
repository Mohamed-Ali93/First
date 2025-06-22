using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace UserManagment.Users;

public interface IExtendedUserAppService : IApplicationService
{
    Task<ExtendedUserDto> GetExtendedUserInfoAsync(string userId);
    Task SetUserStatusAsync(string userId, ExtendedUserStatus status);
}
