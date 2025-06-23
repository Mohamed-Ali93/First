using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace UserManagment.SecurityLogs
{
    [Area(UserManagmentRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = UserManagmentRemoteServiceConsts.RemoteServiceName)]
    [Route("api/user-managment/security-logs")]
    public class SecurityLogController : UserManagmentController
    {
        private readonly ISecurityLogAppService _securityLogAppService;

        public SecurityLogController(ISecurityLogAppService securityLogAppService)
        {
            _securityLogAppService = securityLogAppService;
        }

        [HttpGet]
        [Authorize]
        public Task<PagedResultDto<SecurityLogDto>> GetListAsync([FromQuery] GetSecurityLogInput input)
        {
            return _securityLogAppService.GetListAsync(input);
        }
    }
} 