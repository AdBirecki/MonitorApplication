using System.Threading;
using System.Threading.Tasks;
using MonitorApplication_Scheduler.SchedulingModels.Interfaces;

namespace MonitorApplication_Scheduler.SchedulingModels.Models
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
