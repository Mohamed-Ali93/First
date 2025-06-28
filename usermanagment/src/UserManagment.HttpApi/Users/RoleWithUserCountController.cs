using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Identity;
using UserManagment.Permissions;

namespace UserManagment.Users
{
    [Area(UserManagmentRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = UserManagmentRemoteServiceConsts.RemoteServiceName)]
    [Route("api/user-managment/roles-with-user-count")] 
    public class RoleWithUserCountController : UserManagmentController, IRoleWithUserCountAppService
    {
        private readonly IRoleWithUserCountAppService _service;

        public RoleWithUserCountController(IRoleWithUserCountAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public Task<List<RoleWithUserCountDto>> GetListAsync()
        {
            return _service.GetListAsync();
        }

        // Get users in a specific role
        [HttpGet]
        [Route("{roleId}/users")]
        [Authorize]
        public Task<List<IdentityUserDto>> GetUsersInRoleAsync(string roleId)
        {
            return _service.GetUsersInRoleAsync(roleId);
        }

        // Specific functionality: Move user from one role to another (keeping in both roles)
        [HttpPost]
        [Route("move-user-to-role")]
        [Authorize(Permissions.UserManagmentPermissions.UserRoleManagement)]
        public Task MoveUserToRoleAsync([FromBody] MoveUserToRoleDto input)
        {
            return _service.MoveUserToRoleAsync(input);
        }

        // Move all users from one role to another (keeping them in both roles)
        [HttpPost]
        [Route("move-all-users-to-role")]
        [Authorize(Permissions.UserManagmentPermissions.UserRoleManagement)]
        public Task MoveAllUsersToRoleAsync([FromBody] MoveAllUsersToRoleDto input)
        {
            return _service.MoveAllUsersToRoleAsync(input);
        }
    }
} 