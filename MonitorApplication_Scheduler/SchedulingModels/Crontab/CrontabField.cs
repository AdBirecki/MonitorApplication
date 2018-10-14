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
                if (start < 0)
                {
                    //
                    // We're setting the entire range of values.
                    //

                    if (interval <= 1)
                    {
                        _minValueSet = minValue;
                        _maxValueSet = maxValue;
                        _bits.SetAll(true);
                        return;
                    }

                    start = minValue;
                    end = maxValue;
                }
                else
                {
                    //
                    // We're only setting a single value - check that it is in range.
                    //

                    if (start < minValue)
                    {
                        throw new FormatException(string.Format(
                            "'{0} is lower than the minimum allowable value for this field. Value must be between {1} and {2} (all inclusive).",
                            start, _impl.MinValue, _impl.MaxValue));
                    }

                    if (start > maxValue)
                    {
                        throw new FormatException(string.Format(
                            "'{0} is higher than the maximum allowable value for this field. Value must be between {1} and {2} (all inclusive).",
                            end, _impl.MinValue, _impl.MaxValue));
                    }
                }
            }
            else
            {
                //
                // For ranges, if the start is bigger than the end value then
                // swap them over.
                //

                if (start > end)
                {
                    end ^= start;
                    start ^= end;
                    end ^= start;
                }

                if (start < 0)
                {
                    start = minValue;
                }
                else if (start < minValue)
                {
                    throw new FormatException(string.Format(
                        "'{0} is lower than the minimum allowable value for this field. Value must be between {1} and {2} (all inclusive).",
                        start, _impl.MinValue, _impl.MaxValue));
                }

                if (end < 0)
                {
                    end = maxValue;
                }
                else if (end > maxValue)
                {
                    throw new FormatException(string.Format(
                        "'{0} is higher than the maximum allowable value for this field. Value must be between {1} and {2} (all inclusive).",
                        end, _impl.MinValue, _impl.MaxValue));
                }
            }

            if (interval < 1)
                interval = 1;

            int i;

            //
            // Populate the _bits table by setting all the bits corresponding to
            // the valid field values.
            //

            for (i = start - minValue; i <= (end - minValue); i += interval)
                _bits[i] = true;

            //
            // Make sure we remember the minimum value set so far Keep track of
            // the highest and lowest values that have been added to this field
            // so far.
            //

            if (_minValueSet > start)
                _minValueSet = start;

            i += (minValue - interval);

            if (_maxValueSet < i)
                _maxValueSet = i;
        }
    }
}
