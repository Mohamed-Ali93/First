using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Microsoft.Extensions.Logging;

namespace UserManagment.Users
{
    public class RoleWithUserCountAppService : ApplicationService, IRoleWithUserCountAppService
    {
        private readonly IIdentityRoleRepository _roleRepository;
        private readonly IIdentityUserRepository _userRepository;
        private readonly ILogger<RoleWithUserCountAppService> _logger;

        public RoleWithUserCountAppService(
            IIdentityRoleRepository roleRepository,
            IIdentityUserRepository userRepository,
            ILogger<RoleWithUserCountAppService> logger)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
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
    }
} 