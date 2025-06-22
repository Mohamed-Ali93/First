using UserManagment.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace UserManagment.Permissions;

public class UserManagmentPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(UserManagmentPermissions.GroupName, L("Permission:UserManagment"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<UserManagmentResource>(name);
    }
}
