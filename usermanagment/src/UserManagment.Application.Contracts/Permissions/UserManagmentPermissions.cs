using Volo.Abp.Reflection;

namespace UserManagment.Permissions;

public class UserManagmentPermissions
{
    public const string GroupName = "UserManagment";

    public const string SecurityLogs = GroupName + ".SecurityLogs";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(UserManagmentPermissions));
    }
}
