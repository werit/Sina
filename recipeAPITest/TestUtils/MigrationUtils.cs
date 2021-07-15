using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using recipeAPITest.TestUtils.Fixtures;
using recipies_ms.Db;

namespace recipeAPITest.TestUtils
{
    public static class MigrationUtils
    {
        public static void MigrateDb()
        {
            using var serviceProvider = new ServiceCollection()
                .AddDbContext<RecipeContext>(opt => opt.UseNpgsql(TestSuiteSetup.DbaConnectionString))
                .AddLogging()
                .BuildServiceProvider();

            using var context = serviceProvider.GetRequiredService<RecipeContext>();
            context.Database.ExecuteSqlRaw($"alter database {TestSuiteSetup.DatabaseSchemaName} set jit to off");
            context.Database.Migrate();
        }
    }
}