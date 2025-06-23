using System;
using Volo.Abp.Application.Dtos;

namespace UserManagment.SecurityLogs
{
    public class GetSecurityLogInput : PagedAndSortedResultRequestDto
    {
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
} 