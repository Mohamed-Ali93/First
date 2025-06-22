using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace First.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class FirstDbContextFactory : IDesignTimeDbContextFactory<FirstDbContext>
{
    public FirstDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        FirstEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<FirstDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new FirstDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../First.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
