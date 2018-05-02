using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeSauce.Api
{
    public static class AwesomeServerExtensions
    {
        public static IWebHostBuilder UseAwesomeServer(this IWebHostBuilder hostBuilder, Action<AwesomeServerOptions> options)
        {
            return hostBuilder.ConfigureServices((IServiceCollection services) =>
            {
                services.Configure(options);
                services.AddSingleton<IServer, AwesomeServer>();
            });
        }
    }
}
