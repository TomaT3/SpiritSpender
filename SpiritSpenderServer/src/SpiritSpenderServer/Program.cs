using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SpiritSpenderServer.HardwareControl;
using Microsoft.Extensions.DependencyInjection;
using SpiritSpenderServer.Config.HardwareConfiguration;

namespace SpiritSpenderServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var hardwareConfiguration = host.Services.GetService<IHardwareConfiguration>();
            hardwareConfiguration.LoadHardwareConfiguration().Wait();
            _ = host.Services.GetService<App>();
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
