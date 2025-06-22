using UserManagment.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace UserManagment;

public abstract class UserManagmentController : AbpControllerBase
{
    protected UserManagmentController()
    {
        LocalizationResource = typeof(UserManagmentResource);
    }
}
