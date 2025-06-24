using System;
using Volo.Abp.Application.Dtos;

namespace UserManagment.Users
{
    public class RoleWithUserCountDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public long UserCount { get; set; }
    }
} 