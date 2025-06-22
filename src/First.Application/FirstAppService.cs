using First.Localization;
using Volo.Abp.Application.Services;

namespace First;

/* Inherit your application services from this class.
 */
public abstract class FirstAppService : ApplicationService
{
    protected FirstAppService()
    {
        LocalizationResource = typeof(FirstResource);
    }
}
