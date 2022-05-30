using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sina.messaging.contracts;
using sina.messaging.contracts.MessageBroker.Kafka;
using sina.planning.Db.Models;


namespace sina.planning.Db
{
    public interface IRecipeSchedulingDbContext<T> where T : IRecipeScheduleEntity
    {
        Task<T> AddRecipeScheduleAsync(T recipeScheduleItem, CancellationToken cancellationToken);
        Task<T> GetScheduleByKeyAsync(Guid scheduleKey, CancellationToken cancellationToken);
        
        Task<IEnumerable<T>> GetSchedulesAsync(CancellationToken cancellationToken);

        Task<RecipeItemCreated> AddRecipeAsync(RecipeItemCreated recipeItem,
            CancellationToken cancellationToken);
    }
    public class RecipeSchedulingContext: DbContext, IRecipeSchedulingDbContext<RecipeScheduleItem>
    {
        private const string Topic = "sina-schedule";
        private const string MessageKey = "schedule";
        private readonly IMessageProducer producer;
        public RecipeSchedulingContext(DbContextOptions<RecipeSchedulingContext> options, IMessageProducer producer)
            : base(options)
        {
            this.producer = producer;
        }

        public DbSet<RecipeScheduleItem> RecipeSchedules { get; set; }
        public DbSet<RecipeItemCreated> Recipes { get; set; }
        public async Task<RecipeScheduleItem> AddRecipeScheduleAsync(RecipeScheduleItem recipeScheduleItem, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            RecipeSchedules.Add(recipeScheduleItem);
            await producer.ProduceMessageAsync(Topic,
                new KafkaMessageScheduleCreated
                {
                    MessageKey = MessageKey, MessageValue = new RecipeScheduleCreated
                    {
                        RecipeId = recipeScheduleItem.RecipeKey, 
                        RecipeName = recipeScheduleItem.RecipeName,
                        ScheduleId = recipeScheduleItem.RecipeScheduleKey,
                        ScheduleDatetime = recipeScheduleItem.RecipeScheduleTime,
                        PlannedPortions = recipeScheduleItem.RecipePortions
                    }
                }.ToJson(),
                cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return recipeScheduleItem;
        }

        public async Task<RecipeScheduleItem> GetScheduleByKeyAsync(Guid scheduleKey, CancellationToken cancellationToken)
        {
            return await RecipeSchedules.SingleOrDefaultAsync(sch => sch.RecipeScheduleKey == scheduleKey,
                cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<RecipeScheduleItem>> GetSchedulesAsync(CancellationToken cancellationToken)
        {
            return await RecipeSchedules.ToListAsync(cancellationToken);
        }


        public async Task<RecipeItemCreated> AddRecipeAsync(RecipeItemCreated recipeItem, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Recipes.Add(recipeItem);
            await SaveChangesAsync(cancellationToken);
            return recipeItem;
        }
    }

    
}