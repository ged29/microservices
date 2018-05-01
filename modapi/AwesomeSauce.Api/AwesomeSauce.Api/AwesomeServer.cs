using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;

namespace AwesomeSauce.Api
{
    public class AwesomeServer : IServer
    {
        public AwesomeServer(IOptions<AwesomeServerOptions> options)
        {
            var serverAddressesFeature = new ServerAddressesFeature();
            serverAddressesFeature.Addresses.Add(options.Value.FolderPath);

            Features = new FeatureCollection();
            Features.Set<IServerAddressesFeature>(serverAddressesFeature);
            Features.Set<IHttpRequestFeature>(new HttpRequestFeature());
            Features.Set<IHttpResponseFeature>(new HttpResponseFeature());
        }

        public IFeatureCollection Features { get; }

        public Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var watcher = new AwesomeFolderWatcher<TContext>(application, Features);
                watcher.Watch();
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
