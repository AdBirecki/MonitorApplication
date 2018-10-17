using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using MonitorApplicationHttpClient;
using MonitorApplication_Models;
using MonitorApplication_Scheduler.SchedulingModels.Interfaces;

namespace MonitorApplication_Scheduler.SchedulingModels.Models
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
            GoldData.Current = goldData;
            GoldData.PrintGoldDataTime();
        }
    }

    public class GoldData
    {
        public static GoldDataDto Current { get; set; }

        public static void PrintGoldDataTime()
        {
            Console.WriteLine($" Gold Data Received at : {DateTime.UtcNow}");
        }
    }
}
