﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WinMan.Controllers
{
    public class ProcessManagerController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Index()
        {
            return Lib.ViewResolver.GetResponse("ProcessManager", "Index.cshtml", null, true);
        }
    }
}
