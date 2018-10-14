using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.Scheduling.Crontab
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
