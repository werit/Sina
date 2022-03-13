using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sina.planning.Db.Models;


namespace sina.planning.Db
{
    public interface IRecipeSchedulingDbContext<T> where T : IRecipeScheduleEntity
    {
        Task<T> AddRecipeScheduleAsync(T recipeScheduleItem, CancellationToken cancellationToken);
    }
    public class RecipeSchedulingContext: DbContext, IRecipeSchedulingDbContext<RecipeScheduleItem>
    {
        public RecipeSchedulingContext(DbContextOptions<RecipeSchedulingContext> options)
            : base(options)
        {
        }

        public DbSet<RecipeScheduleItem> RecipeSchedules { get; set; }
        public async Task<RecipeScheduleItem> AddRecipeScheduleAsync(RecipeScheduleItem recipeScheduleItem, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            RecipeSchedules.Add(recipeScheduleItem);
            await SaveChangesAsync(cancellationToken);
            return recipeScheduleItem;
        }
    }

    
}