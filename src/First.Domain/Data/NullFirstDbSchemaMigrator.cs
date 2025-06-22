using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace First.Data;

/* This is used if database provider does't define
 * IFirstDbSchemaMigrator implementation.
 */
public class NullFirstDbSchemaMigrator : IFirstDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
