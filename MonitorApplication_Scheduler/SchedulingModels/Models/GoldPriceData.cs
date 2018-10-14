using MonitorApplication_Scheduler.SchedulingModels.Interfaces;
using MonitorApplicationHttpClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorApplication_Models.Scheduling.Models
{
    public class GoldPriceData : IScheduledTask
    {
        public GoldPriceData(GoldClient goldPriceClient) {

        }

        public string Schedule => "*/5 * * * *";

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
