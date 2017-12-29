﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WinMan.PerformanceCounters.Models;

namespace WinMan.api
{
    public class PerformanceCounterController : ApiController
    {
        [HttpGet]
        public PerformanceCounterModel GetPerformanceCounter()
        {
            return PerformanceCounters.Utils.GetPerformanceCounter(Factory.ProcessorCounter, Factory.MemoryCounter);
        }
    }
}
