using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace UserManagment.Users
{
    public interface IRoleWithUserCountAppService : IApplicationService
    {
        Task<List<RoleWithUserCountDto>> GetListAsync();
    }
} 