using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using recipies_ms.Db.Models;
using sina.messaging.contracts;
using sina.messaging.contracts.MessageBroker.Kafka;

namespace recipies_ms.Db
{
    public interface IRecipeDbContext<T> where T : IRecipeEntity
    {
        Task<T> AddRecipeAsync(T recipeItem, CancellationToken cancellationToken);
        Task<RecordUpdateStatus> UpdateRecipeAsync(T recipeItem, CancellationToken cancellationToken);
        Task<T> GetRecipeByKeyAsync(Guid recipeKey, CancellationToken cancellationToken);
        Task<RecordUpdateStatus> DeleteRecipeByKeyAsync(Guid recipeKey, CancellationToken cancellationToken);

        Task<IEnumerable<T>> GetRecipesAsync(CancellationToken cancellationToken);

        Task<RecipeScheduleCreated> AddScheduleAsync(RecipeScheduleCreated schedulingItem,
            CancellationToken cancellationToken);

        Task<IEnumerable<RecipeScheduleCreated>> GetSchedulesBetweenTimeAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken);
    }

    public class RecipeContext : DbContext, IRecipeDbContext<RecipeItem>
    {
        private const string Topic = "sina";
        private const string MessageKey = "recipe";
        private readonly IMessageProducer producer;

        public RecipeContext(DbContextOptions<RecipeContext> options, IMessageProducer producer)
            : base(options)
        {
            this.producer = producer;
        }

        public DbSet<RecipeItem> Recipes { get; set; }
        
        public DbSet<RecipeScheduleCreated> Schedules { get; set; }


        public async Task<RecipeItem> AddRecipeAsync(RecipeItem recipeItem,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Recipes.Add(recipeItem);
            await SaveChangesAsync(cancellationToken);
            await producer.ProduceMessageAsync(Topic,
                new KafkaMessageItemCreated
                    {MessageKey = MessageKey, MessageValue = new RecipeItemCreated{RecipeId = recipeItem.RecipeKey,Name = recipeItem.RecipeName}}.ToJson(),
                cancellationToken);
            return recipeItem;
        }

        public async Task<RecordUpdateStatus> UpdateRecipeAsync(RecipeItem recipeItem,
            CancellationToken cancellationToken)
        {
            if (recipeItem?.RecipeKey == null || string.IsNullOrEmpty(recipeItem.RecipeName))
            {
                throw new ArgumentNullException($"{nameof(recipeItem)} cannot be null and neiter its id or name.");
            }

            Entry(recipeItem).State = EntityState.Modified;
            try
            {
                await SaveChangesAsync(cancellationToken);
                return RecordUpdateStatus.Updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeItemExists(recipeItem.RecipeKey))
                {
                    return RecordUpdateStatus.NotFound;
                }

                throw;
            }
        }

        public async Task<RecipeItem> GetRecipeByKeyAsync(Guid recipeKey, CancellationToken cancellationToken)
        {
            return await Recipes.Include(rec => rec.Ingredient)
                .SingleOrDefaultAsync(rec => rec.RecipeKey == recipeKey, cancellationToken: cancellationToken);
        }

        public async Task<RecordUpdateStatus> DeleteRecipeByKeyAsync(Guid recipeKey,
            CancellationToken cancellationToken)
        {
            var recipeItem = await GetRecipeByKeyAsync(recipeKey, cancellationToken);
            if (recipeItem == null)
            {
                return RecordUpdateStatus.NotFound;
            }

            Entry(recipeItem).State = EntityState.Deleted;
            try
            {
                await SaveChangesAsync(cancellationToken);
                return RecordUpdateStatus.Deleted;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeItemExists(recipeItem.RecipeKey))
                {
                    return RecordUpdateStatus.NotFound;
                }

                throw;
            }
        }

        public async Task<IEnumerable<RecipeItem>> GetRecipesAsync(CancellationToken cancellationToken)
        {
            return await Recipes.Include(rec => rec.Ingredient).ToListAsync(cancellationToken);
        }

        public async Task<RecipeScheduleCreated> AddScheduleAsync(RecipeScheduleCreated schedulingItem, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Schedules.Add(schedulingItem);
            await SaveChangesAsync(cancellationToken);
            return schedulingItem;
        }

        public async Task<IEnumerable<RecipeScheduleCreated>> GetSchedulesBetweenTimeAsync(
            DateTime froma,
            DateTime to,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var query = from schedule in Schedules.Where(x=>x.ScheduleDatetime >= froma && x.ScheduleDatetime <= to)
                join recipe in  Recipes
                    on schedule.RecipeId equals recipe.RecipeKey
                    group 
            select new {}

            var a = await Schedules.Where(x => x.ScheduleDatetime >= froma && x.ScheduleDatetime <= to)
                .Join(Recipes, schedule => schedule.RecipeId, recipe => recipe.RecipeKey, (sch, rc) 
                    => new
                {
ingredient = rc.Ingredient,
multiplier = sch.PlannedPortions
                }).GroupBy(ing =>ing.ingredient)
            .ToListAsync(cancellationToken);

        }

        private bool RecipeItemExists(Guid id)
        {
            return Recipes.Any(e => e.RecipeKey == id);
        }
    }
}