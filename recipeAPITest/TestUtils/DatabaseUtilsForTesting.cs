using Microsoft.Extensions.DependencyInjection;
using recipeAPITest.TestUtils.Fixtures;
using recipies_ms.Db;

namespace recipeAPITest.TestUtils
{
    public static class DatabaseUtilsForTesting
    {
        public static void ClearDatabase(
            CompleteWebApplicationFactory factory)
        {
            using var scope = factory.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<RecipeContext>();
            ClearPersonTable(context);
            context.SaveChanges();
        }

        private static void ClearPersonTable(RecipeContext context)
        {
            context.Recipes.RemoveRange(context.Recipes);
        }


    }
}