using System;
using System.Collections.Generic;
using System.Text;
using MonitorApplication_Models.Scheduling.Crontab;
using MonitorApplication_Scheduler.SchedulingModels.Interfaces;

namespace MonitorApplication_Models.Scheduling.Models
{
    public class SchedulerTaskWrapper
    {
        public CrontabSchedule Schedule {get; set; }
        public IScheduledTask Task { get; set; }

        public DateTime LastRunTime { get; set; }
        public DateTime NextRunTime { get; set; }

        public void Incerement()
        {
            LastRunTime = NextRunTime;
            NextRunTime = Schedule.GetNextOccurance(NextRunTime);
        }

        public bool ShouldRun(DateTime currentTime)
        {
            return NextRunTime < currentTime && LastRunTime != NextRunTime;
        }
    }
}
