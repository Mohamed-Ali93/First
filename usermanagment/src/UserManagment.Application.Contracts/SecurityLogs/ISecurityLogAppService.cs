using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace UserManagment.SecurityLogs
{
    public interface ISecurityLogAppService : IApplicationService
    {
        Task<PagedResultDto<SecurityLogDto>> GetListAsync(GetSecurityLogInput input);
    }
} 