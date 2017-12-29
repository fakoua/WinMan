﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WinMan.Models;
using WinMan.Services.Models;

namespace WinMan.api
{
    public class ServicesController : ApiController
    {
      public async Task<IEnumerable<ServiceModel>> Get()
        {
            return await Services.Utils.GetServicesAsync();
        }

        [HttpGet]
        public async Task<ServiceModel> Details(string id)
        {
            return await Services.Utils.GetServiceAsync(id);
        }

        [HttpGet]
        public async Task<ExtendedServiceModel> Extended(string id)
        {
            return await Services.Utils.GetExtendedServiceAsync(id);
        }

        [HttpPost]
        public string Start([FromBody] ServiceControlModel service)
        {
            return Services.Utils.ControlService(service.ServiceName, ControlType.StartService).ToString();
        }

        [HttpPost]
        public string Stop([FromBody] ServiceControlModel service)
        {
            return Services.Utils.ControlService(service.ServiceName, ControlType.StopService).ToString();
        }

        [HttpPost]
        public string Pause([FromBody] ServiceControlModel service)
        {
            return Services.Utils.ControlService(service.ServiceName, ControlType.PauseService).ToString();
        }

        [HttpPost]
        public string Resume([FromBody] ServiceControlModel service)
        {
            return Services.Utils.ControlService(service.ServiceName, ControlType.ResumeService).ToString();
        }
    }
}
