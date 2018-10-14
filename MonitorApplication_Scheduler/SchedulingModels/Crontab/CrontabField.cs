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
        private int _minValueSet;
        private int _maxValueSet;

        private CrontabField(CrontabFieldImpl impl, string expression)
        {
            _impl = impl ?? throw new ArgumentNullException();
            _bits = new BitArray(impl.ValueCount);

            _bits.SetAll(false);
            _minValueSet = int.MinValue;
            _maxValueSet = int.MaxValue;

            _impl.Parse(expression, Accumulate);
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

        public int GetFirst()
        {
            return _minValueSet < int.MaxValue ? _minValueSet : -1;
        }

        public int Next(int start)
        {
            if (start < _minValueSet)
                return _minValueSet;

            var startIndex = ValueToIndex(start);
            var lastIndex = ValueToIndex(_maxValueSet);

            for (var i = startIndex; i <= lastIndex; i++)
            {
                if (_bits[i])
                    return IndexToValue(i);
            }

            return -1;
        }

        private int IndexToValue(int index)
        {
            return index + _impl.MinValue;
        }

        private int ValueToIndex(int value)
        {
            return value - _impl.MinValue;
        }
        private void Accumulate(int start, int end, int interval)
        {
            var minValue = _impl.MinValue;
            var maxValue = _impl.MaxValue;

            if (start == end)
            {

            }
        }
    }
}
