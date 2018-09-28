using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Models.Scheduling.Crontab
{
    [Serializable]
    public sealed class CrontabField
    {
        private readonly BitArray _bits;
        private readonly CrontabFieldImpl _impl;

        private CrontabField(CrontabFieldImpl impl, string expression)
        {
            _impl = _impl ?? throw new ArgumentNullException();
            _bits = new BitArray(impl.ValueCount);
        }

        public static CrontabField Minutes(string expression)
        {
            return new CrontabField(CrontabFieldImpl.Minute, expression);
        }

        public static CrontabField Hours(string expression)
        {
            return new CrontabField(CrontabFieldImpl.Hour, expression);
        }

        public static CrontabField DaysOfWeek(string expression)
        {
            return new CrontabField(CrontabFieldImpl.Day, expression);
        }

        public static CrontabField Months(string expression)
        {
            return new CrontabField(CrontabFieldImpl.Month, expression);
        }

        public static CrontabField Days(string expression)
        {
            return new CrontabField(CrontabFieldImpl.Day, expression);
        }
    }
}
