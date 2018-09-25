using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Converters;

namespace MonitorApplicationUtilities
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
