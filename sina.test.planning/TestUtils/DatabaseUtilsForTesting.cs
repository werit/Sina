using Microsoft.Extensions.DependencyInjection;
using sina.planning.Db;
using sina.test.planning.TestUtils.PlanningFixtures;

namespace sina.test.planning.TestUtils
{
    public static class DatabaseUtilsForTesting
    {
        public static void ClearDatabase(
            CompleteWebApplicationFactory factory)
        {
            using var scope = factory.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<RecipeSchedulingContext>();
            ClearPersonTable(context);
            context.SaveChanges();
        }

        private static void ClearPersonTable(RecipeSchedulingContext context)
        {
            context.RecipeSchedules.RemoveRange(context.RecipeSchedules);
        }


    }
}