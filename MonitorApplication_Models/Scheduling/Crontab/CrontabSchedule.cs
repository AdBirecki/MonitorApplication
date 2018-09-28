using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.Scheduling.Crontab
{
    [Serializable]
    public sealed class CrontabSchedule
    {
        private static readonly char[] Separators = {' '};
        private readonly CrontabField _days;
        private readonly CrontabField _daysOfWeek;
        private readonly CrontabField _hours;
        private readonly CrontabField _minutes;
        private readonly CrontabField _months;

        private CrontabSchedule(string expression)
        {
            string[] fields = expression.Split((char[]) Separators, StringSplitOptions.RemoveEmptyEntries);
            if (fields.Length != 5)
            {
                throw  new FormatException(string.Format( $" {expression} is not a valid crontab expression." +
                                                          $" It must contain al least 5 components of a schedule. " +
                                                          $"Valid sequence contains minutes, hours, days, months, days of week. "));
            }

            _minutes = CrontabField.Minutes(fields[0]);
            _hours = CrontabField.Hours(fields[1]);
            _days = CrontabField.Days(fields[2]);
            _months = CrontabField.Months(fields[3]);
            _daysOfWeek = CrontabField.DaysOfWeek(fields[4]);
        }

    }
}
