using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Identity;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace UserManagment.EntityFrameworkCore;

public static class UserManagmentDbContextModelCreatingExtensions
{
    public static void ConfigureUserManagment(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        // Map extended user properties as real columns
        builder.Entity<IdentityUser>(b =>
        {
            b.Property<DateTime?>("LastLoginTime").HasColumnName("LastLoginTime");
            b.Property<int>("LoginAttemptCount").HasColumnName("LoginAttemptCount").HasDefaultValue(0);
            b.Property<int>("UserStatus").HasColumnName("UserStatus").HasDefaultValue(0); // Assuming enum is stored as int
        });

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(UserManagmentDbProperties.DbTablePrefix + "Questions", UserManagmentDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
    }
}
