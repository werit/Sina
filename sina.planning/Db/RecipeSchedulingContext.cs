using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sina.planning.Db.Models;


namespace sina.planning.Db
{
    public interface IRecipeSchedulingDbContext<T> where T : IRecipeScheduleEntity
    {
    }
    public class RecipeSchedulingContext: DbContext, IRecipeSchedulingDbContext<RecipeScheduleItem>
    {
        public RecipeSchedulingContext(DbContextOptions<RecipeSchedulingContext> options)
            : base(options)
        {
        }

        public DbSet<RecipeScheduleItem> RecipeSchedules { get; set; }
    }

    
}