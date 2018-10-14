using MonitorApplication_Scheduler.SchedulingModels.Interfaces;
using MonitorApplicationHttpClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorApplication_Models.Scheduling.Models
{
    public class GoldPriceDataRecoveryTask : IScheduledTask
    {
        private readonly GoldClient _httpClient;
        public GoldPriceDataRecoveryTask(GoldClient goldPriceClient)
        {
            _httpClient = goldPriceClient;
        }

        public string Schedule => "*/1 * * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            GoldDataDto goldData = await _httpClient.GetGoldValues<GoldDataDto>();
        }
    }
}
