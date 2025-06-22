using System;
using Volo.Abp.Identity;

namespace UserManagment.Users;

public static class ExtendedIdentityUserExtensions
{
    public static DateTime? GetLastLoginTime(this IdentityUser user)
    {
        if (user.ExtraProperties.TryGetValue(ExtendedUserClaimTypes.LastLoginTime, out var value) && value is DateTime dt)
            return dt;
        if (user.ExtraProperties.TryGetValue(ExtendedUserClaimTypes.LastLoginTime, out value) && value is string s && DateTime.TryParse(s, out var parsed))
            return parsed;
        return null;
    }

    public static void SetLastLoginTime(this IdentityUser user, DateTime? lastLoginTime)
    {
        user.ExtraProperties[ExtendedUserClaimTypes.LastLoginTime] = lastLoginTime;
    }

    public static int GetLoginAttemptCount(this IdentityUser user)
    {
        if (user.ExtraProperties.TryGetValue(ExtendedUserClaimTypes.LoginAttemptCount, out var value) && value is int i)
            return i;
        if (user.ExtraProperties.TryGetValue(ExtendedUserClaimTypes.LoginAttemptCount, out value) && value is string s && int.TryParse(s, out var parsed))
            return parsed;
        return 0;
    }

    public static void SetLoginAttemptCount(this IdentityUser user, int count)
    {
        user.ExtraProperties[ExtendedUserClaimTypes.LoginAttemptCount] = count;
    }

    public static ExtendedUserStatus GetUserStatus(this IdentityUser user)
    {
        if (user.ExtraProperties.TryGetValue(ExtendedUserClaimTypes.UserStatus, out var value) && value is int i)
            return (ExtendedUserStatus)i;
        if (user.ExtraProperties.TryGetValue(ExtendedUserClaimTypes.UserStatus, out value) && value is string s && int.TryParse(s, out var parsed))
            return (ExtendedUserStatus)parsed;
        return ExtendedUserStatus.PendingApproval;
    }

    public static void SetUserStatus(this IdentityUser user, ExtendedUserStatus status)
    {
        user.ExtraProperties[ExtendedUserClaimTypes.UserStatus] = (int)status;
    }
}
