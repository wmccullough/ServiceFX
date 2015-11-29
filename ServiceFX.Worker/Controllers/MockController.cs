using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceFX.Core;

namespace ServiceFX.Worker.Controllers
{
    public class MockController : ServiceController
    {
        public override void Start()
        {
            foreach (int index in Enumerable.Range(0, 1000))
            {
                EventLogWrapper.WriteEventLog("Potato", EventLogEntryType.Information);
            }
            base.Start();
        }
    }
}
