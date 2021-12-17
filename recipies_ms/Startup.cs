using System;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using recipies_ms.Db;
using recipies_ms.Db.Models;
using recipies_ms.Web.ErrorHandling;

namespace recipies_ms
{
    public class Startup
    {
        private const int DbMaxConnectionRetryCount = 5;
        private const int DbConnectionRetryDelay = 3;
        readonly string MyAllowSpecificOrigins = "allowedOrigins";
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
            services.AddDbContext<RecipeContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("RecipesConnection"),
                    builder => builder.EnableRetryOnFailure(DbMaxConnectionRetryCount,
                        TimeSpan.FromSeconds(DbConnectionRetryDelay), new Collection<string>())));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "recipies_ms", Version = "v1" });
            });

            services.AddScoped<IRecipeDbContext<RecipeItem>, RecipeContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "recipies_ms v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}