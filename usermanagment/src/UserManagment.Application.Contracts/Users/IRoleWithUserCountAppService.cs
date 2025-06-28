using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace UserManagment.Users
{
    public class MoveAllUsersToRoleDto
    {
        public string SourceRoleId { get; set; }
        public string TargetRoleId { get; set; }
    }

    public interface IRoleWithUserCountAppService : IApplicationService
    {
        Task<List<RoleWithUserCountDto>> GetListAsync();
        
        // Get users in a specific role
        Task<List<IdentityUserDto>> GetUsersInRoleAsync(string roleId);
        
        // Specific functionality: Move user from one role to another (keeping in both roles)
        Task MoveUserToRoleAsync(MoveUserToRoleDto input);
        Task MoveAllUsersToRoleAsync(MoveAllUsersToRoleDto input);
    }
} 