using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using recipies_ms.Db;
using sina.planning.Db;
using sina.test.planning.TestUtils.PlanningFixtures;

namespace sina.test.planning.TestUtils
{
    public static class MigrationUtils
    {
        public static void MigrateDb()
        {
            using var serviceProvider = new ServiceCollection()
                .AddDbContext<RecipeSchedulingContext>(opt => opt.UseNpgsql(TestSuiteSetupPlanning.GetConnectionString()))
                .AddLogging()
                .BuildServiceProvider();

            using var context = serviceProvider.GetRequiredService<RecipeSchedulingContext>();
            context.Database.ExecuteSqlRaw($"alter database {TestSuiteSetupPlanning.DatabaseSchemaName} set jit to off");
            context.Database.Migrate();
        }
    }
}