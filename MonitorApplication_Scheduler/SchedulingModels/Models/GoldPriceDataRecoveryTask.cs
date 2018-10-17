using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using MonitorApplicationHttpClient;
using MonitorApplication_Models;
using MonitorApplication_Scheduler.SchedulingModels.Interfaces;

namespace MonitorApplication_Scheduler.SchedulingModels.Models
{
    public class GoldPriceDataRecoveryTask : IScheduledTask
    {
        private readonly GoldClient _httpClient;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public GoldPriceDataRecoveryTask(GoldClient goldPriceClient,
            ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _httpClient = goldPriceClient;
            _logger = _loggerFactory.CreateLogger<string>();
        }

        public string Schedule => "*/1 * * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            GoldDataDto goldData = await _httpClient.GetGoldValues<GoldDataDto>();
            ChangeDto dataChange = goldData.Children.FirstOrDefault();
            double goldValue = dataChange?.XauPrice ?? 0;
            _logger.LogCritical($" Gold Data Received at : {DateTime.UtcNow} is {goldValue}");
        }
    }
}
