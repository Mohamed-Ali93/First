using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using Volo.Abp.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Dynamic.Core;

namespace UserManagment.SecurityLogs
{
    public class SecurityLogAppService : UserManagmentAppService, ISecurityLogAppService
    {
        private readonly IRepository<IdentitySecurityLog, Guid> _securityLogRepository;

        public SecurityLogAppService(IRepository<IdentitySecurityLog, Guid> securityLogRepository)
        {
            _securityLogRepository = securityLogRepository;
        }

        [UnitOfWork]
        [Authorize(Permissions.UserManagmentPermissions.SecurityLogs)]
        public async Task<PagedResultDto<SecurityLogDto>> GetListAsync(GetSecurityLogInput input)
        {
            var queryable = await _securityLogRepository.GetQueryableAsync();
            var query = queryable
                .WhereIf(input.UserId.HasValue, (IdentitySecurityLog x) => x.UserId == input.UserId)
                .WhereIf(!string.IsNullOrWhiteSpace(input.UserName), (IdentitySecurityLog x) => x.UserName.Contains(input.UserName))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Action), (IdentitySecurityLog x) => x.Action == input.Action)
                .WhereIf(input.StartTime.HasValue, (IdentitySecurityLog x) => x.CreationTime >= input.StartTime)
                .WhereIf(input.EndTime.HasValue, (IdentitySecurityLog x) => x.CreationTime <= input.EndTime);

            var totalCount = await AsyncExecuter.CountAsync(query);

            var items = await AsyncExecuter.ToListAsync(
                query
                    .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? nameof(IdentitySecurityLog.CreationTime) + " DESC" : input.Sorting)
                    .PageBy(input)
            );

            return new PagedResultDto<SecurityLogDto>(
                totalCount,
                ObjectMapper.Map<List<IdentitySecurityLog>, List<SecurityLogDto>>(items)
            );
        }
    }
} 