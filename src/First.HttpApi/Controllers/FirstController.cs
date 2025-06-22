using First.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace First.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class FirstController : AbpControllerBase
{
    protected FirstController()
    {
        LocalizationResource = typeof(FirstResource);
    }
}
