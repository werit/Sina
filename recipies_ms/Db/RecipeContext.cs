using Microsoft.EntityFrameworkCore;
using recipies_ms.Db.Models;

namespace recipies_ms.Db
{
    public interface IRecipeDbContext
    {

    }

    public class RecipeContext : DbContext, IRecipeDbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options)
            : base(options)
        {
        }

        public DbSet<RecipeItem> Recipes { get; set; }
    }
}