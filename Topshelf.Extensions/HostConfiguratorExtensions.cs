using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Topshelf.HostConfigurators;

namespace Topshelf
{
    public static class HostConfiguratorExtensions
    {
        public static void RegisterUnhandledExceptionHandler(this HostConfigurator hostConfigurator)
        {
            string performanceCounterCategory = Assembly.GetEntryAssembly().GetName().Name;
            string performanceCounterName = "UnhandledExceptions";


            if (!PerformanceCounterCategory.Exists(performanceCounterCategory))
            {
                PerformanceCounterCategory.Create(performanceCounterCategory, "",
                    PerformanceCounterCategoryType.SingleInstance, performanceCounterName, "");
            }

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            IncrementUnhandledExceptions();
        }

        private static long GetCurrentValue()
        {
            try
            {
                string performanceCounterCategory = Assembly.GetEntryAssembly().GetName().Name;
                string performanceCounterName = "UnhandledExceptions";

                PerformanceCounter counter = new PerformanceCounter();
                counter.CategoryName = performanceCounterCategory;
                counter.CounterName = performanceCounterName;
                counter.ReadOnly = true;
                long current = counter.RawValue;
                counter.Close();
                return current;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private static void IncrementUnhandledExceptions()
        {
            try
            {
                string performanceCounterCategory = Assembly.GetEntryAssembly().GetName().Name;
                string performanceCounterName = "UnhandledExceptions";

                PerformanceCounter counter = new PerformanceCounter();
                counter.CategoryName = performanceCounterCategory;
                counter.CounterName = performanceCounterName;
                counter.ReadOnly = false;
                counter.Increment();
                counter.Close();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
