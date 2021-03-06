﻿using System;
using System.Globalization;
using System.IO;

namespace MonitorApplication_Scheduler.SchedulingModels.Crontab
{
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

        public DateTime GetNextOccurrence(DateTime baseTime)
        {
            return GetNextOccurrence(baseTime, DateTime.MaxValue);
        }

        private static Calendar Calendar
        {
            get { return CultureInfo.InvariantCulture.Calendar; }
        }

        public DateTime GetNextOccurrence(DateTime baseTime, DateTime endTime)
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

            if (minute == nil)
            {
                minute = _minutes.GetFirst();
                hour++;
            }

            hour = _hours.Next(hour);

            if (hour == nil)
            {
                minute = _minutes.GetFirst();
                hour = _hours.GetFirst();
                day++;
            }
            else if (hour > baseHour)
            {
                minute = _minutes.GetFirst();
            }

            day = _days.Next(day);

            RetryDayMonth:

            if (day == nil)
            {
                minute = _minutes.GetFirst();
                hour = _hours.GetFirst();
                day = _days.GetFirst();
                month++;
            }
            else if (day > baseDay)
            {
                minute = _minutes.GetFirst();
                hour = _hours.GetFirst();
            }

            month = _months.Next(month);

            if (month == nil)
            {
                minute = _minutes.GetFirst();
                hour = _hours.GetFirst();
                day = _days.GetFirst();
                month = _months.GetFirst();
                year++;
            }
            else if (month > baseMonth)
            {
                minute = _minutes.GetFirst();
                hour = _hours.GetFirst();
                day = _days.GetFirst();
            }

            var dateChanged = day != baseDay || month != baseMonth || year != baseYear;

            if (day > 28 && dateChanged && day > Calendar.GetDaysInMonth(year, month))
            {
                if (year >= endYear && month >= endMonth && day >= endDay)
                    return endTime;

                day = nil;
                goto RetryDayMonth;
            }

            var nextTime = new DateTime(year, month, day, hour, minute, 0, 0, baseTime.Kind);

            if (nextTime >= endTime)
                return endTime;

            if (_daysOfWeek.Contains((int)nextTime.DayOfWeek))
                return nextTime;

            return GetNextOccurrence(new DateTime(year, month, day, 23, 59, 0, 0, baseTime.Kind), endTime);
        }

        public override string ToString()
        {
            var writer = new StringWriter(CultureInfo.InvariantCulture);

            _minutes.Format(writer, true);
            writer.Write(' ');
            _hours.Format(writer, true);
            writer.Write(' ');
            _days.Format(writer, true);
            writer.Write(' ');
            _months.Format(writer, true);
            writer.Write(' ');
            _daysOfWeek.Format(writer, true);

            return writer.ToString();
        }
    }
}
