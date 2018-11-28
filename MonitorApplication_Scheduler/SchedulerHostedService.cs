using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MonitorApplication_Scheduler.SchedulingModels.Crontab;
using MonitorApplication_Scheduler.SchedulingModels.Interfaces;
using MonitorApplication_Scheduler.SchedulingModels.Models;

namespace MonitorApplication_Scheduler
{
    public class SchedulerHostedService : HostedService
    {
        private readonly  List<SchedulerTaskWrapper> _scheduledTasks = new List<SchedulerTaskWrapper>();
        private readonly ILogger _logger;
        private const string serviceName = nameof(SchedulerHostedService);

        public SchedulerHostedService(IEnumerable<IScheduledTask> scheduledTasks, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SchedulerHostedService>();

            DateTime referenceTime = DateTime.Now;
            foreach (var scheduledTask in scheduledTasks)
            {
                _scheduledTasks.Add(new SchedulerTaskWrapper
                {
                    Schedule = CrontabSchedule.Parse(scheduledTask.Schedule),
                    Task = scheduledTask,
                    NextRunTime = referenceTime
                });
            }
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await ExecuteOnceAsync(cancellationToken);

                await Task.Delay(TimeSpan.FromSeconds(15), cancellationToken);
            }
        }

        private async Task ExecuteOnceAsync(CancellationToken cancellationToken)
        {
            var taskFactory = new TaskFactory(TaskScheduler.Current);
            var referenceTime = DateTime.Now;

            var tasksThatShouldRun = _scheduledTasks
                .Where(t => t.ShouldRun(referenceTime))
                .ToList();

            foreach (var taskThatShouldRun in tasksThatShouldRun)
            {
                taskThatShouldRun.Increment();

                await taskFactory.StartNew(
                    async () =>
                    {
                        try
                        {
                         await taskThatShouldRun.Task.ExecuteAsync(cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            _logger.Log(LogLevel.Error, $" {serviceName} caused an error: {ex.Message} ");
                        }
                    },
                    cancellationToken);
            }
        }
    }
}
