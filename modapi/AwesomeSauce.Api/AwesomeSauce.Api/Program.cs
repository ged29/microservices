using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AwesomeSauce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHostCustomly(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static IWebHost BuildWebHostCustomly(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(config => config.AddJsonFile("appSettings.json", true))
                .ConfigureLogging(logging => logging.AddConsole().AddDebug())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
        }
    }
}
