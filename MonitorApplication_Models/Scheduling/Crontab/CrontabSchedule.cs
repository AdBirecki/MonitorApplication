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

        public static CrontabSchedule Parse(string expression)
        {
            if (expression == null)
            {
                throw  new ArgumentNullException(nameof(expression));
            }
            return new CrontabSchedule(expression);
        }

        public DateTime GetNextOccurance(DateTime baseTime)
        {
            return GetNextOccurance(baseTime, DateTime.MaxValue);
        }

        public DateTime GetNextOccurance(DateTime baseTime, DateTime endTime)
        {
            const int nil = -1;

            var baseYear = baseTime.Year;
            var baseMonth = baseTime.Month;
            var baseDay = baseTime.Day;
            var baseHour = baseTime.Hour;
            var baseMinute = baseTime.Minute;

            var endYear = endTime.Year;
            var endMonth = endTime.Month;
            var endDay = endTime.Day;

            var year = baseYear;
            var month = baseMonth;
            var day = baseDay;
            var hour = baseHour;
            var minute = baseMinute + 1;

            minute = _minutes.Next(minute);
            hour = _hours.Next(hour);
            day = _days.Next(day);
            month = _months.Next(month);

            return GetNextOccurance(new DateTime(year, month, day, 23, 59, 0, 0, baseTime.Kind), endTime);

        }
    }
}
