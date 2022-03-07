using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sina.planning;
using sina.planning.Db;

namespace sina.test.planning.TestUtils.PlanningFixtures
{
    public class CompleteWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<RecipeSchedulingContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<RecipeSchedulingContext>(opt => opt.UseNpgsql(TestSuiteSetupPlanning.GetConnectionString()));
            });
        }
    }
}