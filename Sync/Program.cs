using MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<DatabaseOptions>(hostContext.Configuration.GetSection("MongoDb"));
                    services.AddSingleton<DatabasesConfiguration>();
                    //services.AddSingleton<SyncService>();
                    services.AddSingleton<MessageBusService>();
                    services.AddHostedService<Worker>();
                });
    }
}
