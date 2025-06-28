using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using Volo.Abp.Authorization;
using Microsoft.AspNetCore.Authorization;
using UserManagment.Permissions;

namespace UserManagment.Users
{
    public class RoleWithUserCountAppService : ApplicationService, IRoleWithUserCountAppService
    {
        private readonly IIdentityRoleRepository _roleRepository;
        private readonly IIdentityUserRepository _userRepository;
        private readonly ExtendedIdentityUserManager _userManager;
        private readonly ILogger<RoleWithUserCountAppService> _logger;

        public RoleWithUserCountAppService(
            IIdentityRoleRepository roleRepository,
            IIdentityUserRepository userRepository,
            ExtendedIdentityUserManager userManager,
            ILogger<RoleWithUserCountAppService> logger)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<RoleWithUserCountDto>> GetListAsync()
        {
            try
            {
                var roles = await _roleRepository.GetListAsync();
                var result = new List<RoleWithUserCountDto>();

                foreach (var role in roles)
                {
                    // Fix: Use the correct overload of GetCountAsync with the roleId parameter
                    var userCount = await _userRepository.GetCountAsync(roleId: role.Id);
                    result.Add(new RoleWithUserCountDto
                    {
                        Id = role.Id,
                        Name = role.Name,
                        UserCount = userCount
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetListAsync: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<List<IdentityUserDto>> GetUsersInRoleAsync(string roleId)
        {
            try
            {
                var role = await _roleRepository.GetAsync(Guid.Parse(roleId));
                var users = await _userRepository.GetListAsync(roleId: role.Id);
                
                return users.Select(user => new IdentityUserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    PhoneNumber = user.PhoneNumber,
                    IsActive = user.IsActive,
                    LockoutEnabled = user.LockoutEnabled,
                    CreationTime = user.CreationTime,
                    LastModificationTime = user.LastModificationTime
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting users for role {RoleId}: {Message}", roleId, ex.Message);
                throw;
            }
        }

        [UnitOfWork]
        [Authorize(Permissions.UserManagmentPermissions.UserRoleManagement)]
        public async Task MoveUserToRoleAsync(MoveUserToRoleDto input)
        {
            try
            {
                var user = await _userRepository.GetAsync(Guid.Parse(input.UserId));
                var sourceRole = await _roleRepository.GetAsync(Guid.Parse(input.SourceRoleId));
                var targetRole = await _roleRepository.GetAsync(Guid.Parse(input.TargetRoleId));

                // Check if user is already in the target role
                var isInTargetRole = await _userManager.IsInRoleAsync(user, targetRole.Name);
                if (isInTargetRole)
                {
                    _logger.LogWarning("User {UserName} is already in role {TargetRoleName}", user.UserName, targetRole.Name);
                    return;
                }

                // Add user to target role (don't remove from source role as per requirement)
                var result = await _userManager.AddToRoleAsync(user, targetRole.Name);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to add user to role: {errors}");
                }

                _logger.LogInformation("Successfully moved user {UserName} from role {SourceRoleName} to role {TargetRoleName}", 
                    user.UserName, sourceRole.Name, targetRole.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error moving user {UserId} from role {SourceRoleId} to role {TargetRoleId}: {Message}", 
                    input.UserId, input.SourceRoleId, input.TargetRoleId, ex.Message);
                throw;
            }
        }
    }
} 