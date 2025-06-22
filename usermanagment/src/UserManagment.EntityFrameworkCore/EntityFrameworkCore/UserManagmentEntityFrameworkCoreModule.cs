using Volo.Abp.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace UserManagment.EntityFrameworkCore;

[DependsOn(
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(UserManagmentDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class UserManagmentEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<UserManagmentDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
            
            /* Add custom repositories here. Example:
            * options.AddRepository<Question, EfCoreQuestionRepository>();
            */
        });
    }
}
