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

        public static HttpResponseMessage GetResponse(string controller, string view, object model, bool useTemplate)
        {
            string html = Template(controller , view, useTemplate );
            string cachKey = $"c-{controller}.{view}";
            string result = "";
            if (model==null)
            {
                result = html;
            } else
            {
                result = Engine.Razor.RunCompile(html, cachKey, null, model);
            }
            var response = new HttpResponseMessage();
            response.Content = new StringContent(result);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        public static string Template(string controller, string view, bool useTemplate)
        {
            var appPath = AssemblyDirectory;
            string htmlTemplate = "";
            if (useTemplate)
            {
                string templatePath = Path.Combine(appPath, "Views");
                templatePath = Path.Combine(templatePath, "Common");
                templatePath = Path.Combine(templatePath, "Template.html");
                
                using (FileStream stream = new FileStream(templatePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        htmlTemplate =reader.ReadToEnd();
                    }
                }
            }

            string resourcePath = Path.Combine(appPath, "Views");
            resourcePath = Path.Combine(resourcePath, controller);
            resourcePath = Path.Combine(resourcePath, view);
            string htmlBody = "";
            using (FileStream stream = new FileStream(resourcePath , FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    htmlBody =  reader.ReadToEnd();
                }
            }

            if (useTemplate )
            {
                htmlTemplate = htmlTemplate.Replace("$SectionStyle", GetStyle(htmlBody));
                htmlTemplate = htmlTemplate.Replace("$SectionScript", GetScript(htmlBody));

                htmlBody = CleanBody(htmlBody);
                htmlTemplate = htmlTemplate.Replace("$SectionBody", htmlBody);

                return htmlTemplate;
            }
            else
            {
                return htmlBody;
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

        private static string CleanBody(string htmlBody)
        {
            var brakets = GetBrackets(htmlBody, "$SectionStyle");
            var pre = htmlBody.Substring(0, brakets.Item1);
            var post = htmlBody.Substring(brakets.Item2+1);
            var rtnVal = pre + post;
            rtnVal = rtnVal.Replace("$SectionStyle", "");

            brakets = GetBrackets(rtnVal, "$SectionScript");
            pre = rtnVal.Substring(0, brakets.Item1);
            post = rtnVal.Substring(brakets.Item2 + 1);
            rtnVal = pre + post;
            rtnVal = rtnVal.Replace("$SectionScript", "");

            return rtnVal;
        }

        private static string GetStyle(string htmlBody)
        {
            var brakets = GetBrackets(htmlBody, "$SectionStyle");
            var startBrakets = brakets.Item1;
            var endBrakets = brakets.Item2;
            var rtnVal = htmlBody.Substring(startBrakets+1, endBrakets - startBrakets-2);
            return rtnVal;
        }

        private static string GetScript(string htmlBody)
        {
            var brakets = GetBrackets(htmlBody, "$SectionScript");
            var startBrakets = brakets.Item1;
            var endBrakets = brakets.Item2;
            var rtnVal = htmlBody.Substring(startBrakets + 1, endBrakets - startBrakets - 2);
            return rtnVal;
        }

        private static Tuple<int, int> GetBrackets(string htmlBody, string sectionName)
        {
            var startPos = htmlBody.IndexOf(sectionName);
            var startBrakets = htmlBody.IndexOf("{", startPos);
            var endBrakets = 0;

            var opening = 1;
            var cursor = startBrakets + 1;

            do
            {
                var c = htmlBody.Substring(cursor, 1);
                if (c == "{") { opening++; }
                if (c == "}") { opening--; }
                if (opening == 0)
                {
                    endBrakets = cursor;
                }
                cursor++;
            } while (opening > 0);
            return new Tuple<int, int>(startBrakets, endBrakets);
        }
    }
}
