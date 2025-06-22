using First.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace First.Permissions;

public class FirstPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FirstPermissions.GroupName);

        var booksPermission = myGroup.AddPermission(FirstPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(FirstPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(FirstPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(FirstPermissions.Books.Delete, L("Permission:Books.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(FirstPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FirstResource>(name);
    }
}
