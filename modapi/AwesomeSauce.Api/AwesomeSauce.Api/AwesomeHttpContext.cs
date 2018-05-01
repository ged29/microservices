using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace AwesomeSauce.Api
{
    internal class AwesomeHttpContext : HttpContext
    {
        private IFeatureCollection features;
        private string fullPath;

        public AwesomeHttpContext(IFeatureCollection features, string fullPath)
        {
            this.features = features;
            this.fullPath = fullPath;
        }
    }
}