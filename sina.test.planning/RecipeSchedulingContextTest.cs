using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sina.planning.Db;
using sina.planning.Db.Models;
using sina.test.planning.TestUtils;
using sina.test.planning.TestUtils.PlanningFixtures;
using Xunit;

namespace sina.test.planning
{
    [Collection(CommonFixtures.DatabaseCollectionName)]
    public class RecipeSchedulingContextTest : IClassFixture<CompleteWebApplicationFactory>, IDisposable
    {
        private readonly CompleteWebApplicationFactory factory;

        public RecipeSchedulingContextTest(CompleteWebApplicationFactory factory)
        {
            this.factory = factory;

            DatabaseUtilsForTesting.ClearDatabase(factory);
        }
        // TODO: do a real test + fix me
       /* [Fact]
        public async Task AddRecipeScheduleAsyncTest()
        {
            await using var context = new RecipeSchedulingContext(new DbContextOptionsBuilder<RecipeSchedulingContext>()
                .UseNpgsql(TestSuiteSetupPlanning.GetConnectionString()).Options);
            await context.AddRecipeScheduleAsync(new RecipeScheduleItem()
            {
                RecipeScheduleKey = Guid.NewGuid(),
                RecipeScheduleTime = DateTime.Now,
                RecipeKey = Guid.NewGuid(),
                RecipeName = "No anme",
                RecipePortions = 12
            },CancellationToken.None);
            
            Assert.Equal(1,await context.RecipeSchedules.CountAsync(CancellationToken.None));
        }*/
        
        public void Dispose()
        {
            DatabaseUtilsForTesting.ClearDatabase(factory);
         
        }
    }
}