using System;

namespace MonitorApplication_Scheduler.SchedulingModels.Crontab
{
    [Serializable]
    public enum CrontabFieldKind
    {
        Minute,
        Hour,
        Day,
        Month,
        DayOfWeek
    }
}
