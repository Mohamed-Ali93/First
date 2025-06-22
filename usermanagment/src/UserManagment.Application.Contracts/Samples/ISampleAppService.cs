using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace UserManagment.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
