using System.Web.Http;
using System.Web.Http.Cors;

namespace api_backend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            // var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Other Web API configuration code here
        }
    }
}
