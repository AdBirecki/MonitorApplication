using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Utilities.Helpers
{
    public static class TimeStampHelper
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime GetDateTime(this double timestamp) {
            DateTime dateTime = _epoch.AddMilliseconds(timestamp).ToLocalTime();
            return dateTime;
        }

        public static double GetDateTimeNoEpoch(this DateTime dateTime) {
            double timeDiff = dateTime.Subtract(_epoch.ToLocalTime()).TotalMilliseconds;
            return timeDiff;
        }
    }
}
