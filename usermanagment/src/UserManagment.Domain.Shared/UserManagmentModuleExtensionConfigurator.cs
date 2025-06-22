using System;
using System.ComponentModel.DataAnnotations;
using UserManagment.Users;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace UserManagment;

public static class UserManagmentModuleExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ConfigureIdentityUser();
        });
    }
    
    private static void ConfigureIdentityUser()
    {
        ObjectExtensionManager.Instance.Modules()
            .ConfigureIdentity(identity =>
            {
                identity.ConfigureUser(user =>
                {
                    user.AddOrUpdateProperty<DateTime?>(
                        ExtendedUserClaimTypes.LastLoginTime,
                        property =>
                        {
                            property.Attributes.Add(new DisplayAttribute
                            {
                                Name = "Last Login Time",
                                Description = "The last time user logged in"
                            });
                        }
                    );

                    user.AddOrUpdateProperty<int>(
                        ExtendedUserClaimTypes.LoginAttemptCount,
                        property =>
                        {
                            property.DefaultValue = 0;
                            property.Attributes.Add(new DisplayAttribute
                            {
                                Name = "Login Attempt Count",
                                Description = "Number of login attempts"
                            });
                        }
                    );

                    user.AddOrUpdateProperty<ExtendedUserStatus>(
                        ExtendedUserClaimTypes.UserStatus,
                        property =>
                        {
                            property.DefaultValue = ExtendedUserStatus.PendingApproval;
                            property.Attributes.Add(new DisplayAttribute
                            {
                                Name = "User Status",
                                Description = "Current status of the user"
                            });
                        }
                    );
                });
            });
    }
}