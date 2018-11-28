using System;
using MonitorApplication_Scheduler.SchedulingModels.Crontab;
using MonitorApplication_Scheduler.SchedulingModels.Interfaces;

namespace MonitorApplication_Scheduler.SchedulingModels.Models
{
    public class SchedulerTaskWrapper
    {
        public CrontabSchedule Schedule {get; set; }
        public IScheduledTask Task { get; set; }

        public DateTime LastRunTime { get; set; }
        public DateTime NextRunTime { get; set; }

        public void Increment()
        {
            LastRunTime = NextRunTime;
            NextRunTime = Schedule.GetNextOccurrence(NextRunTime);
        }

        public bool ShouldRun(DateTime currentTime)
        {
            return NextRunTime < currentTime && LastRunTime != NextRunTime;
        }
    }
}
