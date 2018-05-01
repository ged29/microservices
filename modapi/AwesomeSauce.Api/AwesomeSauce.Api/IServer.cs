using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeSauce.Api
{
    public interface IServer
    {
        IFeatureCollection Features { get; }
        Task StartAsync<TContext>(IHttpApplication<TContext> application, CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
