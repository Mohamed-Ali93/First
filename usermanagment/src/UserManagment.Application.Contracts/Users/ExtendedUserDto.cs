using System;

namespace UserManagment.Users;

public class ExtendedUserDto
{
    public DateTime? LastLoginTime { get; set; }
    public int LoginAttemptCount { get; set; }
    public ExtendedUserStatus UserStatus { get; set; }
}
