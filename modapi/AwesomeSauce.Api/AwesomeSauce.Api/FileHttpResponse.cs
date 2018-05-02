using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AwesomeSauce.Api
{
    internal class FileHttpResponse : HttpResponse
    {
        private readonly string fullPath;

        public FileHttpResponse(HttpContext httpContext, string fullPath)
        {            
            HttpContext = httpContext;
            Body = new MemoryStream();
            this.fullPath = fullPath;
        }

        public override HttpContext HttpContext { get; }

        public override int StatusCode { get; set; }
        public override IHeaderDictionary Headers { get; }
        public override Stream Body { get; set; }
        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }
        public override IResponseCookies Cookies { get; }
        public override bool HasStarted { get; }
        //
        // Summary:
        //     Adds a delegate to be invoked after the response has finished being sent to the client.
        //
        // Parameters:
        //   callback:
        //     The delegate to invoke.
        //
        //   state:
        //     A state object to capture and pass back to the delegate.
        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            using (var reader = new StreamReader(Body))
            {
                Body.Position = 0;

                var text = reader.ReadToEnd();
                File.WriteAllText(fullPath, $"{this.StatusCode} - {text}");

                Body.Flush();
                Body.Dispose();
            }
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }
        public override void Redirect(string location, bool permanent)
        {
            throw new NotImplementedException();
        }
    }
}