using System;
using Volo.Abp.Application.Dtos;

namespace UserManagment.Users
{
    public class MoveUserToRoleDto : EntityDto<Guid>
    {
        public string UserId { get; set; }
        public string SourceRoleId { get; set; }
        public string TargetRoleId { get; set; }
        public string UserName { get; set; }
        public string SourceRoleName { get; set; }
        public string TargetRoleName { get; set; }
    }
} 