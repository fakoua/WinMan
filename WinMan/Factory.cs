using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMan
{
    class Factory
    {
        private static PerformanceCounter  _processorCounter;

        public static PerformanceCounter ProcessorCounter {
            get
            {
                if (_processorCounter ==null)
                {
                    var processorCategory = PerformanceCounterCategory.GetCategories()
             .FirstOrDefault(cat => cat.CategoryName == "Processor");

                    var countersInCategory = processorCategory.GetCounters("_Total");
                    _processorCounter = countersInCategory.First(cnt => cnt.CounterName == "% Processor Time");
                }
                return _processorCounter;
            }

        }
    }
}
