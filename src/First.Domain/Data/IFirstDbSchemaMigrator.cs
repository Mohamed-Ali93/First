using System.Threading.Tasks;

namespace First.Data;

public interface IFirstDbSchemaMigrator
{
    Task MigrateAsync();
}
