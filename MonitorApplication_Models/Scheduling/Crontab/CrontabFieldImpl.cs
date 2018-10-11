using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using MonitorApplication_Models.Scheduling.Accumulator;

namespace MonitorApplication_Models.Scheduling.Crontab
{   /* https://blog.maartenballiauw.be/post/2017/08/01/building-a-scheduled-cache-updater-in-aspnet-core-2.html */
    public class CrontabFieldImpl : IObjectReference
    {
        public static readonly CrontabFieldImpl Minute = new CrontabFieldImpl(CrontabFieldKind.Minute, 0, 59, null);
        public static readonly CrontabFieldImpl Hour = new CrontabFieldImpl(CrontabFieldKind.Hour, 0, 23, null);
        public static readonly CrontabFieldImpl Day = new CrontabFieldImpl(CrontabFieldKind.Day, 1, 31, null);

        public static readonly CrontabFieldImpl Month = new CrontabFieldImpl(CrontabFieldKind.Month, 1, 12,
            new[]
            {
                "January", "February", "March", "April",
                "May", "June", "July", "August",
                "September", "October", "November",
                "December"
            });

        public static readonly CrontabFieldImpl DayOfWeek = new CrontabFieldImpl(CrontabFieldKind.DayOfWeek, 0, 6, new[]
        {
            "Sunday", "Monday", "Tuesday",
            "Wednesday", "Thursday", "Friday",
            "Saturday"
        });

        public object GetRealObject(StreamingContext context)
        {
            throw new NotImplementedException();
        }

        private CrontabFieldImpl(CrontabFieldKind kind, int minValue, int maxValue, string[] names)
        {
            _kind = kind;
            _minValue = minValue;
            _maxValue = maxValue;
            _names = names;
        }

        public void Parse(string str, CrontabFieldAccumulator acc)
        {
            if (acc == null)
                throw new ArgumentNullException(nameof(acc));

            if (string.IsNullOrEmpty(str))
                return;
            try
            {
                InternalParse(str, acc);
            }
            catch (Exception e)
            {
                throw new FormatException($"'{str}' is not a valid crontab field expression.", e);
            }
        }

        public CrontabFieldKind Kind
        {
            get { return _kind; }
        }

        public int MinValue
        {
            get { return _minValue; }
        }

        public int MaxValue
        {
            get { return _maxValue; }
        }

        public int ValueCount
        {
            get { return _maxValue - _minValue + 1; }
        }

        private void InternalParse(string str, CrontabFieldAccumulator acc)
        {
            if (str.Length == 0)
                throw new FormatException("A crontab field value cannot be empty.");

            int commaIndex = str.IndexOf(",", StringComparison.Ordinal);
            if (commaIndex > 0)
            {
                foreach (var token in str.Split(Comma))
                {
                    InternalParse(token,acc);
                }
            }
            else
            {
                var every = 1;
                var slashIndex = str.IndexOf("/", StringComparison.Ordinal);

                if (slashIndex > 0)
                {

                }
            }
        }

        private readonly char[] Comma = {','};
        private readonly CrontabFieldKind _kind;
        private readonly int _maxValue;
        private readonly int _minValue;
        private readonly string[] _names;
    }
}
