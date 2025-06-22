using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace UserManagment.EntityFrameworkCore;

[ConnectionStringName(UserManagmentDbProperties.ConnectionStringName)]
public interface IUserManagmentDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
