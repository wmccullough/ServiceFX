using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFX.Core
{
    public abstract class ServiceController
    {
        public virtual void Start()
        {
            EventLogWrapper.WriteEventLog("Service started", EventLogEntryType.Information);
        }

        public virtual void Stop()
        {
            EventLogWrapper.WriteEventLog("Service stopped", EventLogEntryType.Information);
        }
    }
}
