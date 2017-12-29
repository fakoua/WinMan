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
            return Lib.ViewResolver.GetResponse("Account", "Login.cshtml", null, false);
        }

        [HttpPost]
        public HttpResponseMessage Login([FromBody]LoginModel login)
        {
            var uri = Request.RequestUri.Authority;

            if (login.Username.Equals("admin", StringComparison.InvariantCultureIgnoreCase ) & login.Password.Equals(Factory.PassCode ) )
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, "WinMan Admin"));
                var id = new ClaimsIdentity(claims, "WinMan-Auth");

                var auth = Request.GetOwinContext().Authentication;
                auth.SignIn(id);
                
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri($"http://{uri}/home");
                return response;
            }
             else
            {
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location = new Uri($"http://{uri}/Account/Login?error=incorrect");
                return response;
            }
           
        }
    }
}
