using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using MonitorApplicationHttpClient;
using MonitorApplication_Models;
using MonitorApplication_Scheduler.SchedulingModels.Interfaces;
using MonitorApplication_BL.Commands.Interfaces;
using MonitorApplication_BL.Commands.Commands;

namespace MonitorApplication_Scheduler.SchedulingModels.Models
{
    public class GoldPriceDataRecoveryTask : IScheduledTask
    {
        private readonly GoldClient _httpClient;
        private readonly ILogger _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public GoldPriceDataRecoveryTask(
            GoldClient goldPriceClient,
            ILoggerFactory loggerFactory,
            ICommandDispatcher commandDispatcher)
        {
            _httpClient = goldPriceClient;
            _logger = loggerFactory.CreateLogger<GoldPriceDataRecoveryTask>();
            _commandDispatcher = commandDispatcher;
        }

        public string Schedule => "*/1 * * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            GoldDataDto goldData = await _httpClient.GetGoldValues<GoldDataDto>();
            ChangeDto dataChange = goldData.Children.FirstOrDefault();

            SaveMineralDataCommand saveGoldDataCommand = new SaveMineralDataCommand(dataChange, goldData.TimeStamp);
            if(saveGoldDataCommand.IsValid())
            {
                _commandDispatcher.Execute(saveGoldDataCommand);
            }

            double goldValue = dataChange?.XauPrice ?? 0;
            _logger.LogWarning($" Gold Data Received at : {DateTime.Now} is {goldValue}");
        }
    }
}
