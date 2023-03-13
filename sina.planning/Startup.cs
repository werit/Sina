using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using sina.endpoint.common.Web.ErrorHandling;
using sina.messaging.contracts.MessageBroker.Extensions;
using sina.planning.Db;
using sina.planning.Db.Models;
using sina.planning.MicroserviceConsumer;

namespace sina.planning
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opt =>
            {
                opt.SuppressAsyncSuffixInActionNames = false;
            });
            services.AddDbContext<RecipeSchedulingContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("RecipesScheduleConnection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "sina.planning", Version = "v1"});
            });
            
            services.AddScoped<IRecipeSchedulingDbContext<RecipeScheduleItem>, RecipeSchedulingContext>();
            services.AddSinaKafkaProducer();
            services.AddSinaKafkaConsumers("sina-planners","sina");
            services.AddSingleton<EventPusher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "sina.planning v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.ApplicationServices.GetService<EventPusher>();
        }
    }
}