using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using Swashbuckle.Application;
using System.Web.Http;
using WinMan.Lib;
using Microsoft.Owin.Security.Cookies;
using System;
using Microsoft.Owin;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System.Reflection;
using System.IO;
using RazorEngine;

namespace WinMan
{
    class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var cookieAuthOptions = new CookieAuthenticationOptions
            {
                AuthenticationType = "WinMan-Auth",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromSeconds(5),
                SlidingExpiration = true,
                CookieSecure = CookieSecureOption.SameAsRequest,
                LoginPath = new PathString("/Account/Login")
            };
            appBuilder.UseCookieAuthentication(cookieAuthOptions);

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action="get", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                "Default", "{controller}/{action}",
                new { controller = "Home", action = "Index" });

            config.EnableSwagger(c=>
            {
                c.DocumentFilter<LowercaseDocumentFilter>();
                c.SingleApiVersion("v1", "WinMan");
            }).EnableSwaggerUi();

            var physicalFileSystem = new PhysicalFileSystem(@".\wwwroot");
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem
            };
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[] { "default.html" };
            appBuilder.UseFileServer(options);

            appBuilder.UseCompressionModule();
            appBuilder.UseWebApi(config);
        }
    }
}

