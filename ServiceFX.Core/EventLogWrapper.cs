using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFX.Core
{
    public class EventLogWrapper
    {
        public static string EventSource = $"{Assembly.GetEntryAssembly().GetName().Name}, Version: {Assembly.GetEntryAssembly().GetName().Version}";
        public static string LogGroup = "My Application";

        public static void WriteEventLog(string message, EventLogEntryType type)
        {
            if (!EventLog.SourceExists(EventSource))
                EventLog.CreateEventSource(EventSource, LogGroup);

            EventLog.WriteEntry(EventSource, message, type);
            LogDebug(message);
        }

        [Conditional("DEBUG")]
        public static void LogDebug(string message)
        {
            string output = $"[DEBUG]: {message}";
            Debug.WriteLine(output);
            Console.WriteLine(output);
        }

        private static bool TryDelete(string name)
        {
            try {
                EventLog.Delete(name);
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        private static bool TryDeleteEventSource(string name)
        {
            try {
                EventLog.DeleteEventSource(name);
                return true;
            }
            catch (Exception) {
                return false;
            }
        }
    }
}
