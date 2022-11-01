using Catalog.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;

namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .Enrich.WithProperty("Application", "HY.CaseStudy")
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .MinimumLevel.Override("System", LogEventLevel.Warning)
                            .MinimumLevel.Verbose()
                            .WriteTo.Console()
                            .CreateLogger();

                Log.Information("Uygulama çalýþmaya hazýrlanýyor.");

                var host = CreateHostBuilder(args).Build();

                using var scope = host.Services.CreateScope();
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ICatalogContext>();

                CatalogContextSeed.SeedData(context.Categories, context.Products);

                Log.Information("Host çalýþtýrýlýyor. localhost:5000");

                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Uygulama beklenmedik bir þekilde kapandý.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging(config => config.ClearProviders())
            .UseSerilog();
    }
}
