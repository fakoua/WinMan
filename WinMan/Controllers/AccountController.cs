using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WinMan.Models;

namespace WinMan.Controllers
{
    public class AccountController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Login()
        {
            var model = new { Name = "World", Email = "someone@somewhere.com" };
            return Lib.ViewResolver.GetResponse("Account", "Login.cshtml", null, false);
        }

        [HttpPost]
        public HttpResponseMessage Login([FromBody]LoginModel login)
        {

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "Brock"));
            claims.Add(new Claim(ClaimTypes.Email, "brockallen@gmail.com"));
            var id = new ClaimsIdentity(claims, "WinMan-Auth");

            var auth = Request.GetOwinContext().Authentication;
            auth.SignIn(id);

            var uri = Request.RequestUri.Authority;
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri($"http://{uri}/api/storage/drives");
            return response;
        }
    }
}
