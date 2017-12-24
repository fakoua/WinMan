using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WinMan.Controllers
{
    public class AccountController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Login()
        {
            string template = "Hello @Model.Name! Welcome to Web API and Razor!";
            var result =  Engine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });

            var response = new HttpResponseMessage();
            response.Content = new StringContent(result);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
