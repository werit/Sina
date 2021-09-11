using Microsoft.EntityFrameworkCore;
using sina.planning.Db.Models;


namespace sina.planning.Db
{
    public interface IRecipeSchedulingDbContext
    {
    }
    public class RecipeSchedulingContext: DbContext, IRecipeSchedulingDbContext
    {
        public RecipeSchedulingContext(DbContextOptions<RecipeSchedulingContext> options)
            : base(options)
        {
        }

        public DbSet<RecipeScheduleItem> RecipeSchedules { get; set; }
        
    }

    
}