using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using recipies_ms.Db.Models;

namespace recipies_ms.Db
{
    public interface IRecipeDbContext
    {
        Task<RecipeItem> AddRecipe(string recipeName, string recipeDesc, CancellationToken cancellationToken);
    }

    public class RecipeContext : DbContext, IRecipeDbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options)
            : base(options)
        {
        }

        public DbSet<RecipeItem> Recipes { get; set; }


        public async Task<RecipeItem> AddRecipe(string recipeName, string recipeDesc,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var recipeItem = new RecipeItem
            {
                RecipeKey = new Guid(),
                RecipeName = recipeName,
                RecipeDescription = recipeDesc
            };
            Recipes.Add(recipeItem);
            await SaveChangesAsync(cancellationToken);
            return recipeItem;
        }
    }
}