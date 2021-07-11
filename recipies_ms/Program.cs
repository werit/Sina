using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using recipies_ms.Db;

namespace recipies_ms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            var host = CreateHostBuilder(args).Build();
            try
            {
                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;

                try
                {
                    using var context = services.GetRequiredService<RecipeContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred during database migration.");
                    throw;
                }

                logger.Info("Starting...");

                host.Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Main program crashed and is being shut down.");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); }).ConfigureLogging(
                    logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    })
                .UseNLog();
    }
}