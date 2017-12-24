using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WinMan.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Index()
        {
            var model = new { Name = "World", Email = "someone@somewhere.com" };
            return Lib.ViewResolver.GetResponse("Home", "Index.cshtml", model);
        }

        [HttpGet]
        public HttpResponseMessage About()
        {
            var model = new { Name = "Sameh", Email = "someone@somewhere.com" };
            return Lib.ViewResolver.GetResponse("Home", "About.cshtml", model);
        }
    }
}
