using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinMan.PerformanceCounters.Models;

namespace WinMan.PerformanceCounters
{
    public class Utils
    {
        public static PerformanceCounterModel GetPerformanceCounter(PerformanceCounter processorCounter)
        {
            var rtnVal = new PerformanceCounterModel();
            rtnVal.Processor = processorCounter.NextValue();
            return rtnVal;
        }
    }
}
