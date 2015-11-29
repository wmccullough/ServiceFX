using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceFX.Core;
using ServiceFX.Worker.Controllers;
using Topshelf;

namespace ServiceFX.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 
            {
                x.Service<ServiceController>(s =>                        
                {
                    s.ConstructUsing(name => new MockController());     
                    s.WhenStarted(tc => tc.Start());              
                    s.WhenStopped(tc => tc.Stop());               
                });
                x.RunAsLocalSystem();                            
                x.RegisterUnhandledExceptionHandler();
                x.SetDescription("Sample Topshelf Host");        
                x.SetDisplayName("Stuff");                       
                x.SetServiceName("Stuff");                       
            });
        }
    }
}
