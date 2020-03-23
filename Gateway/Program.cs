using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Gateway.Aggregates;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    // Doplnìní Ocelot konfigurace
                    //.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                    // doplnìní ocelot konfigurací (ve formátu: (?i)ocelot.([a-zA-Z0-9]*).json)
                    .AddOcelot($"{Directory.GetCurrentDirectory()}/OcelotConfigs", hostingContext.HostingEnvironment)
                    .AddEnvironmentVariables();
                Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();
            })
            .ConfigureServices(s => {
                s.AddOcelot()
                .AddSingletonDefinedAggregator<SkladZasobyAgregator>();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddSerilog(dispose: true);
                logging.AddConsole();
                logging.AddDebug();
            })
            .UseIISIntegration()
            .Configure(app =>
            {
                app.UseOcelot().Wait();
            })
            .Build()
            .Run();
        }
    }
}
