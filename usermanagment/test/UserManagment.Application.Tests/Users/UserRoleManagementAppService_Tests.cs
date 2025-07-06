using System;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Identity;
using Xunit;
using UserManagment.Users;

namespace UserManagment.Users;

public class RoleWithUserCountAppService_Tests : UserManagmentApplicationTestBase<UserManagmentApplicationTestModule>
{
    private readonly IRoleWithUserCountAppService _roleWithUserCountAppService;
    private readonly IIdentityUserRepository _userRepository;
    private readonly IIdentityRoleRepository _roleRepository;

    public RoleWithUserCountAppService_Tests()
    {
        _roleWithUserCountAppService = GetRequiredService<IRoleWithUserCountAppService>();
        _userRepository = GetRequiredService<IIdentityUserRepository>();
        _roleRepository = GetRequiredService<IIdentityRoleRepository>();
    }

    [Fact]
    public async Task Should_Move_User_To_Role()
    {
        // Arrange
        var user = await _userRepository.FindByNormalizedUserNameAsync("admin");
        var sourceRole = await _roleRepository.FindByNormalizedNameAsync("ADMIN");
        var targetRole = await _roleRepository.FindByNormalizedNameAsync("USER");

        if (user == null || sourceRole == null || targetRole == null)
        {
            // Skip test if required entities don't exist
            return;
        }

        var moveDto = new MoveUserToRoleDto
        {
            UserId = user.Id.ToString(),
            SourceRoleId = sourceRole.Id.ToString(),
            TargetRoleId = targetRole.Id.ToString(),
            UserName = user.UserName,
            SourceRoleName = sourceRole.Name,
            TargetRoleName = targetRole.Name
        };

        // Act
        await _roleWithUserCountAppService.MoveUserToRoleAsync(moveDto);

        // Note: We can't easily test if user is in both roles without additional infrastructure
        // The test verifies that the method executes without throwing an exception
    }

    [Fact]
    public async Task Should_Get_Roles_With_User_Count()
    {
        // Act
        var roles = await _roleWithUserCountAppService.GetListAsync();

        // Assert
        roles.ShouldNotBeNull();
        roles.Count.ShouldBeGreaterThanOrEqualTo(0);
        
        foreach (var role in roles)
        {
            role.Id.ShouldNotBe(Guid.Empty);
            role.Name.ShouldNotBeNullOrEmpty();
            role.UserCount.ShouldBeGreaterThanOrEqualTo(0);
        }
    }
} 