# User Role Management

This module extends the existing `RoleWithUserCountAppService` to provide a specific functionality: **moving users between roles while maintaining their presence in multiple roles**.

## Architecture

### Extended Service Approach
Instead of creating a separate service, we extended the existing `IRoleWithUserCountAppService` and `RoleWithUserCountAppService` to include only the specific functionality that doesn't already exist in ABP Identity.

### Why This Approach?
- **ABP Identity already provides**: Basic user-role management operations through `IdentityRoleService` and `IdentityUserService`
- **We only add**: The specific "move user between roles" functionality that keeps users in both roles
- **Avoids duplication**: No need to reimplement what ABP Identity already provides

## Features

### 1. Move Users Between Roles (New Functionality)
- **Functionality**: Move a user from one role to another role
- **Behavior**: The user is added to the target role while remaining in the source role
- **Example**: Moving user "admin" from "Super Admin" role to "Test User" role results in the user being present in both roles

### 2. Role User Count (Existing)
- **Functionality**: Get roles with their user counts
- **Information**: Role name, user count

## API Endpoints

### Extended RoleWithUserCount Controller
Base URL: `/api/user-managment/roles-with-user-count`

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| GET | `/` | Get roles with user count | Default |
| POST | `/move-user-to-role` | Move user from one role to another | UserRoleManagement |

## Service Implementation

### IRoleWithUserCountAppService (Extended Interface)
```csharp
public interface IRoleWithUserCountAppService : IApplicationService
{
    // Existing method
    Task<List<RoleWithUserCountDto>> GetListAsync();
    
    // New specific functionality
    Task MoveUserToRoleAsync(MoveUserToRoleDto input);
}
```

### RoleWithUserCountAppService (Extended Implementation)
The service implementation includes:
- **Dependency Injection**: Uses `ExtendedIdentityUserManager` for user operations
- **Unit of Work**: All database operations are wrapped in Unit of Work
- **Authorization**: The move user method requires `UserRoleManagement` permission
- **Logging**: Comprehensive logging for audit trails
- **Validation**: Input validation and error handling

## DTOs

### MoveUserToRoleDto
```csharp
public class MoveUserToRoleDto : EntityDto<Guid>
{
    public string UserId { get; set; }
    public string SourceRoleId { get; set; }
    public string TargetRoleId { get; set; }
    public string UserName { get; set; }
    public string SourceRoleName { get; set; }
    public string TargetRoleName { get; set; }
}
```

### RoleWithUserCountDto (Existing)
```csharp
public class RoleWithUserCountDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public long UserCount { get; set; }
}
```

## Frontend Integration

### Angular Service Updates
The `UserManagmentService` has been updated to use the extended endpoint:

```typescript
// Specific functionality: Move user from one role to another (keeping in both roles)
moveUserToRole(input: MoveUserToRoleDto) {
  return this.restService.request<MoveUserToRoleDto, void>(
    { method: 'POST', url: '/api/user-managment/roles-with-user-count/move-user-to-role', body: input },
    { apiName: this.apiName }
  );
}
```

## Permissions

### UserRoleManagement Permission
- **Name**: `UserManagment.UserRoleManagement`
- **Description**: Allows access to user role management functionality
- **Required**: The move user operation requires this permission
- **Existing**: The original `GetListAsync()` method doesn't require this permission

## Key Implementation Details

### 1. Service Extension Pattern
- **Approach**: Extended existing service instead of creating new one
- **Benefits**: 
  - Maintains consistency with existing architecture
  - Reduces code duplication
  - Follows single responsibility principle for role-related operations

### 2. Multi-Role Support
The system supports users being assigned to multiple roles simultaneously. When moving a user from role A to role B, the user remains in role A and is also added to role B.

### 3. Validation
- Checks if user exists before operations
- Checks if role exists before operations
- Prevents duplicate role assignments
- Validates user is in role before removal

### 4. Logging
All operations are logged with appropriate levels:
- **Information**: Successful operations
- **Warning**: Duplicate assignments or missing entities
- **Error**: Failed operations with details

### 5. Unit of Work
All database operations are wrapped in Unit of Work to ensure data consistency.

## What We Don't Implement

Since ABP Identity already provides these functionalities, we don't duplicate them:

- **Get users in role**: Use `IIdentityUserRepository.GetListAsync(roleId: roleId)`
- **Get user roles**: Use `IdentityUserManager.GetRolesAsync(user)`
- **Add user to role**: Use `IdentityUserManager.AddToRoleAsync(user, roleName)`
- **Remove user from role**: Use `IdentityUserManager.RemoveFromRoleAsync(user, roleName)`
- **Check if user in role**: Use `IdentityUserManager.IsInRoleAsync(user, roleName)`

## Testing

### Test Coverage
- User role movement functionality
- Get roles with user count (existing functionality)

### Running Tests
```bash
cd usermanagment
dotnet test test/UserManagment.Application.Tests/Users/RoleWithUserCountAppService_Tests.cs
```

## Security Considerations

1. **Authorization**: The move user endpoint requires the `UserRoleManagement` permission
2. **Input Validation**: All inputs are validated before processing
3. **SQL Injection Protection**: Uses parameterized queries through Entity Framework
4. **Audit Trail**: All operations are logged for audit purposes

## Usage Example

```csharp
// Move user from one role to another (keeping in both roles)
var moveDto = new MoveUserToRoleDto
{
    UserId = "user-guid",
    SourceRoleId = "source-role-guid", 
    TargetRoleId = "target-role-guid",
    UserName = "admin",
    SourceRoleName = "Super Admin",
    TargetRoleName = "Test User"
};

await roleWithUserCountAppService.MoveUserToRoleAsync(moveDto);
```

## Benefits of This Approach

1. **Minimal Code**: Only implements what's not already available
2. **Leverages ABP**: Uses existing ABP Identity infrastructure
3. **Maintainable**: Less code to maintain
4. **Consistent**: Follows ABP patterns and conventions
5. **Focused**: Addresses only the specific requirement

## Future Enhancements

1. **Bulk Operations**: Support for moving multiple users at once
2. **Role Hierarchy**: Support for role inheritance
3. **Temporary Assignments**: Time-based role assignments
4. **Approval Workflow**: Require approval for role changes
5. **Notification System**: Notify users of role changes 