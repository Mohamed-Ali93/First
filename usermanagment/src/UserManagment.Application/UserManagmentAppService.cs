using UserManagment.Localization;
using Volo.Abp.Application.Services;

namespace UserManagment;

public abstract class UserManagmentAppService : ApplicationService
{
    protected UserManagmentAppService()
    {
        LocalizationResource = typeof(UserManagmentResource);
        ObjectMapperContext = typeof(UserManagmentApplicationModule);
    }
}
