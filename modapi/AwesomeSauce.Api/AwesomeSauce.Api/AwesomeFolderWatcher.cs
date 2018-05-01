using System;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting.Internal;
using System.Threading.Tasks;

namespace AwesomeSauce.Api
{
    public class AwesomeFolderWatcher<TContext>
    {
        private readonly FileSystemWatcher watcher;
        private readonly IHttpApplication<TContext> application;
        private readonly IFeatureCollection features;

        public AwesomeFolderWatcher(IHttpApplication<TContext> application, IFeatureCollection features)
        {
            var path = features.Get<IServerAddressesFeature>().Addresses.FirstOrDefault();
            this.watcher = new FileSystemWatcher(path);
            this.watcher.EnableRaisingEvents = true;

            this.application = application;
            this.features = features;
        }

        public void Watch()
        {
            this.watcher.Created += async (object sender, FileSystemEventArgs e) =>
            {
                var ctx = (HostingApplication.Context)(object)this.application.CreateContext(this.features);
                ctx.HttpContext = new AwesomeHttpContext(this.features, e.FullPath);

                await application.ProcessRequestAsync((TContext)(object)ctx);
                ctx.HttpContext.Response.OnCompleted(null, null);
            };

            Task.Run(() => this.watcher.WaitForChanged(WatcherChangeTypes.All));
        }
    }
}