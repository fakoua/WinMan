using Owin;
using Swashbuckle.Application;
using System.Web.Http;
using WinMan.Lib;

namespace WinMan
{
    class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action="get", id = RouteParameter.Optional }
            );
            config.EnableSwagger(c=>
            {
                c.DocumentFilter<LowercaseDocumentFilter>();
                c.SingleApiVersion("v1", "WinMan");
            }).EnableSwaggerUi();

            appBuilder.UseWebApi(config);
        }
    }
}

