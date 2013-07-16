using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;

namespace CurrentDesk.BackOfficeMamService
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {

            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("FIXExceptionRecoveryService entry point called", "Information");

            bool _stop = false;
            while (true)
            {
                if (!_stop)
                {
                    _stop = true;                   
                    BOMAMManagement boMAMManagement = new BOMAMManagement();
                    boMAMManagement.StartThreads();                   
                }
                Thread.Sleep(10000);
                Trace.WriteLine("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
