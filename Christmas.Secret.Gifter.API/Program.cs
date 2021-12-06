using Albergue.Administrator.HostedServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Albergue.Administrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseKestrel()
                    .UseStartup<Startup>();
                    //.UseUrls("http://[::1]:5020"); WIP
                })
                .ConfigureServices(services =>
                {
                    //services.AddHostedService<TimedHostedService>();
                });
    }
}
