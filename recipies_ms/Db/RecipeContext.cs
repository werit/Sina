using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using recipies_ms.Db.Models;

namespace recipies_ms.Db
{
    public interface IRecipeDbContext
    {
        Task<RecipeItem> AddRecipeAsync(string recipeName, string recipeDesc, CancellationToken cancellationToken);
        Task<UpdateStatus> UpdateRecipeAsync(RecipeItem recipeItem, CancellationToken cancellationToken);
    }

    public class RecipeContext : DbContext, IRecipeDbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options)
            : base(options)
        {
        }

        public DbSet<RecipeItem> Recipes { get; set; }


        public async Task<RecipeItem> AddRecipeAsync(string recipeName, string recipeDesc,
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

        public async Task<UpdateStatus> UpdateRecipeAsync(RecipeItem recipeItem, CancellationToken cancellationToken)
        {
            if (recipeItem?.RecipeKey == null || string.IsNullOrEmpty(recipeItem.RecipeName))
            {
                throw new ArgumentNullException($"{nameof(recipeItem)} cannot be null and neiter its id or name.");
            }

            Entry(recipeItem).State = EntityState.Modified;
            try
            {
                await SaveChangesAsync(cancellationToken);
                return UpdateStatus.Updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeItemExists(recipeItem.RecipeKey))
                {
                    return UpdateStatus.NotFound;
                }

                throw;
            }
        }

        private bool RecipeItemExists(Guid id)
        {
            return Recipes.Any(e => e.RecipeKey == id);
        }
    }
}