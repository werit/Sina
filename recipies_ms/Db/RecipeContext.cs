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
    public class GroupedIngredients
    {
        public Guid IngredientKey { get; set; }
        public string IngredientName { get; set; }
        public string IngredientUnit { get; set; }
        public float IngredientAmount { get; set; }
    }

    public interface IRecipeDbContext<T> where T : IRecipeEntity
    {
        Task<T> AddRecipeAsync(T recipeItem, CancellationToken cancellationToken);
        Task<RecordUpdateStatus> UpdateRecipeAsync(T recipeItem, CancellationToken cancellationToken);
        Task<T> GetRecipeByKeyAsync(Guid recipeKey, CancellationToken cancellationToken);
        Task<RecordUpdateStatus> DeleteRecipeByKeyAsync(Guid recipeKey, CancellationToken cancellationToken);

        Task<IEnumerable<T>> GetRecipesAsync(CancellationToken cancellationToken);

        Task<RecipeScheduleCreated> AddScheduleAsync(RecipeScheduleCreated schedulingItem,
            CancellationToken cancellationToken);

        Task<IEnumerable<GroupedIngredients>> GetSchedulesBetweenTimeAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken);
    }

    public interface IRecipeIngredient
    {
        Task<Ingredient> AddIngredientAsync(Ingredient ingredientItem, CancellationToken cancellationToken);
        Task<RecordUpdateStatus> UpdateIngredientAsync(Ingredient ingredientItem, CancellationToken cancellationToken);
        Task<Ingredient> GetIngredientByKeyAsync(Guid ingredientKey, CancellationToken cancellationToken);
        Task<RecordUpdateStatus> DeleteIngredientByKeyAsync(Guid ingredientKey, CancellationToken cancellationToken);

        Task<IEnumerable<Ingredient>> GetIngredientsAsync(CancellationToken cancellationToken);
    }

    public class RecipeContext : DbContext, IRecipeDbContext<RecipeItem>, IRecipeIngredient
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
        
        public DbSet<Ingredient> Ingredients { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<RecipeIngredientItem>()
                .HasKey(ri => new {ri.RecipeItemId,ri.IngredientId});
            
            modelBuilder.Entity<Ingredient>()
                .HasOne(e => e.ingredientNutritionalValue)
                .WithOne(d => d.Ingredient)
                .HasForeignKey<IngredientNutrition>(f => f.NutritionKey)
                .IsRequired(false);
        }


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

        public async Task<IEnumerable<GroupedIngredients>> GetSchedulesBetweenTimeAsync(
            DateTime froma,
            DateTime to,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            // var query = from schedule in Schedules.Where(x=>x.ScheduleDatetime >= froma && x.ScheduleDatetime <= to)
            //     join recipe in  Recipes.Include(x=>x.Ingredient)
            //         on schedule.RecipeId equals recipe.RecipeKey
            //         group 
            // select new {}
            // List<{Guid,ICollection<RecipeIngredientItem>, float}> b;

            return await Schedules.Where(x => x.ScheduleDatetime >= froma && x.ScheduleDatetime <= to)
                .Join(
                    Recipes.Include(ing => ing.Ingredient)
                        .SelectMany(rec => rec.Ingredient, (x, y) =>
                            new {x.RecipeKey, y.IngredientId, y.Amount, y.Unit}),
                    schedule => schedule.RecipeId, recipe => recipe.RecipeKey,
                    (sch, rc)
                        => new
                        {
                            recipeKey = rc.RecipeKey,
                            ingredientKey = rc.IngredientId,
                            amount = rc.Amount,
                            unit = rc.Unit,
                            portions = sch.PlannedPortions
                        }
                ).Join(
                    Ingredients,
                    combinedTable=>combinedTable.ingredientKey,
                    ingRelComb=>ingRelComb.IngredientKey,
                    (recRelComb,ingredient)
                    => new
                    {
                        recRelComb.recipeKey,
                        recRelComb.ingredientKey,
                        ingredientName = ingredient.IngredientName,
                        recRelComb.amount,
                        recRelComb.unit,
                        recRelComb.portions
                    })
                .GroupBy(ing => new {ing.ingredientKey,ing.ingredientName, ing.unit
        },

        ing => new {ing.amount,ing.portions},
                    (groupBase,groupedResult)=>
                    new GroupedIngredients
                {
                    IngredientKey = groupBase.ingredientKey,
                    IngredientName = groupBase.ingredientName,
                    IngredientUnit = groupBase.unit,
                    IngredientAmount = groupedResult.Sum( res=>res.amount *res.portions)
                })
            .ToListAsync(cancellationToken);

        }

        private bool RecipeItemExists(Guid id)
        {
            return Recipes.Any(e => e.RecipeKey == id);
        }


        public async Task<Ingredient> AddIngredientAsync(Ingredient ingredientItem, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Ingredients.AddAsync(ingredientItem, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return ingredientItem;
        }

        public async Task<RecordUpdateStatus> UpdateIngredientAsync(Ingredient ingredientItem, CancellationToken cancellationToken)
        {
            if (ingredientItem?.IngredientKey == null || string.IsNullOrEmpty(ingredientItem.IngredientName))
            {
                throw new ArgumentNullException($"{nameof(ingredientItem)} cannot be null and neiter its id or name.");
            }

            Entry(ingredientItem).State = EntityState.Modified;

            if (ingredientItem.ingredientNutritionalValue != null)
                Entry(ingredientItem.ingredientNutritionalValue).State =
                    NutritionItemExists(ingredientItem.ingredientNutritionalValue.NutritionKey)
                        ? EntityState.Modified
                        : EntityState.Added;
            try
            {
                await SaveChangesAsync(cancellationToken);
                return RecordUpdateStatus.Updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientItemExists(ingredientItem.IngredientKey))
                {
                    return RecordUpdateStatus.NotFound;
                }

                throw;
            }
        }

        public async Task<Ingredient> GetIngredientByKeyAsync(Guid ingredientKey, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Ingredients.Include(x=>x.ingredientNutritionalValue)
                .SingleOrDefaultAsync(ing => ing.IngredientKey == ingredientKey, cancellationToken);

        }

        public Task<RecordUpdateStatus> DeleteIngredientByKeyAsync(Guid ingredientKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ingredient>> GetIngredientsAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        private bool IngredientItemExists(Guid id)
        {
            return Ingredients.Any(e => e.IngredientKey == id);
        }
        private bool NutritionItemExists(Guid id)
        {
            return Ingredients.Any(e => e.ingredientNutritionalValue.NutritionKey == id);
        }
    }
}