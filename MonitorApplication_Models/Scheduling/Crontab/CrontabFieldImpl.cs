using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MonitorApplication_Models.Scheduling.Crontab
{   /* https://blog.maartenballiauw.be/post/2017/08/01/building-a-scheduled-cache-updater-in-aspnet-core-2.html */
    public class CrontabFieldImpl : IObjectReference
    {
        public static readonly  CrontabFieldImpl Minute = new CrontabFieldImpl(CrontabFieldKind.Minute, 0, 59, null);
        public static readonly  CrontabFieldImpl Hour = new CrontabFieldImpl(CrontabFieldKind.Hour, 0, 23, null);
        public static readonly  CrontabFieldImpl Day = new CrontabFieldImpl(CrontabFieldKind.Day, 1, 31, null);
        public static readonly  CrontabFieldImpl Month = new CrontabFieldImpl(CrontabFieldKind.Month, 1,12, 
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

        public int ValueCount
        {
            get { return _maxValue - _minValue + 1; }
        }
        private CrontabFieldImpl(CrontabFieldKind kind, int minValue, int maxValue, string[] names)
        {
            _kind = kind;
            _minValue = minValue;
            _maxValue = maxValue;
            _names = names;
        }

        private readonly CrontabFieldKind _kind;
        private readonly int _maxValue;
        private readonly int _minValue;
        private readonly string[] _names;
    }
}
