using MonitorApplication_Scheduler.SchedulingModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorApplication_Models.Scheduling.Models
{
    public class SomeOtherTask : IScheduledTask
    {
        public string Schedule => "0 5 * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(5000, cancellationToken);
        }
    }
}
