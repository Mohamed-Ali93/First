using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace UserManagment.EntityFrameworkCore;

[ConnectionStringName(UserManagmentDbProperties.ConnectionStringName)]
public class UserManagmentDbContext : AbpDbContext<UserManagmentDbContext>, IUserManagmentDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public UserManagmentDbContext(DbContextOptions<UserManagmentDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureUserManagment();
    }
}
