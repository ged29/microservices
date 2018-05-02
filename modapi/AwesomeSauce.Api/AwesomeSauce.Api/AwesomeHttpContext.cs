using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;

namespace AwesomeSauce.Api
{
    /// <summary>
    /// A specific override of HttpContext created especially for the file system
    /// </summary>
    public class AwesomeHttpContext : HttpContext
    {
        /// <param name="features">Represents a collection of HTTP features.</param>
        /// <param name="fullPath">Fully qualifed path of the affected file or directory.</param>
        public AwesomeHttpContext(IFeatureCollection features, string fullPath)
        {
            Features = features;
            Request = new FileHttpRequest(this, fullPath);
            Response = new FileHttpResponse(this, fullPath);
        }

        public override IFeatureCollection Features { get; }
        public override HttpRequest Request { get; }
        public override HttpResponse Response { get; }

        public override ConnectionInfo Connection => throw new NotImplementedException();
        public override WebSocketManager WebSockets => throw new NotImplementedException();
        public override AuthenticationManager Authentication => throw new NotImplementedException();
        public override ClaimsPrincipal User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IDictionary<object, object> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IServiceProvider RequestServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override CancellationToken RequestAborted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string TraceIdentifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISession Session { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }
}