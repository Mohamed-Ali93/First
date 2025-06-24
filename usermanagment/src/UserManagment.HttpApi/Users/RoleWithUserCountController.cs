using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace UserManagment.Users
{
    [Area(UserManagmentRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = UserManagmentRemoteServiceConsts.RemoteServiceName)]
    [Route("api/user-managment/roles-with-user-count")] 
    public class RoleWithUserCountController : UserManagmentController
    {
        private readonly IRoleWithUserCountAppService _service;

        public RoleWithUserCountController(IRoleWithUserCountAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public Task<List<RoleWithUserCountDto>> GetListAsync()
        {
            return _service.GetListAsync();
        }
    }
} 