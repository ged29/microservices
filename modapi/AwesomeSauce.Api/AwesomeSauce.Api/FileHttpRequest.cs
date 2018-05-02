using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AwesomeSauce.Api
{
    public class FileHttpRequest : HttpRequest
    {
        public FileHttpRequest(HttpContext httpContext, string fullPath)
        {
            HttpContext = httpContext;

            var firstLine = File.ReadLines(fullPath).FirstOrDefault();
            var requestInstruction = firstLine.Split(" ");

            Method = requestInstruction[0];
            Path = requestInstruction[1];
        }

        public override HttpContext HttpContext { get; }
        public override string Method { get; set; }
        public override PathString Path { get; set; }

        public override string Scheme { get; set; }
        public override bool IsHttps { get; set; }
        public override HostString Host { get; set; }
        public override PathString PathBase { get; set; }
        public override QueryString QueryString { get; set; }
        public override IQueryCollection Query { get; set; }
        public override string Protocol { get; set; }

        public override IHeaderDictionary Headers => new HeaderDictionary();

        public override IRequestCookieCollection Cookies { get; set; }
        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }
        public override Stream Body { get; set; }
        public override IFormCollection Form { get; set; }
        public override bool HasFormContentType => throw new System.NotImplementedException();

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }
    }
}