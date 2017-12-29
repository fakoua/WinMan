using System.Net.Http;
using System.Web.Http;

namespace WinMan.Controllers
{
    [Authorize]
    public class ServiceManagerController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Index()
        {
            return Lib.ViewResolver.GetResponse("ServiceManager", "Index.cshtml", null, true);
        }
    }
}
