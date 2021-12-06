using Microsoft.AspNetCore.Hosting;
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
                    //.UseUrls("http://[::1]:5020"); When not deployed to Heroku use it
                })
                .ConfigureServices(services =>
                {
                    //services.AddHostedService<TimedHostedService>();
                });
    }
}
