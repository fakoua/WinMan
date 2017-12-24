using RazorEngine;
using RazorEngine.Templating;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace WinMan.Lib
{
    class ViewResolver
    {

        public static HttpResponseMessage GetResponse(string controller, string view, object model)
        {
            string html = Template(controller , view);
            string cachKey = $"c-{controller}.{view}";
            string result = Engine.Razor.RunCompile(html, cachKey, null, model);
            var response = new HttpResponseMessage();
            response.Content = new StringContent(result);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        public static string Template(string controller, string view)
        {
            var appPath = AssemblyDirectory;

            string resourcePath = Path.Combine(appPath, "Views");
            resourcePath = Path.Combine(resourcePath, controller);
            resourcePath = Path.Combine(resourcePath, view);

            using (FileStream stream = new FileStream(resourcePath , FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
                    }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
