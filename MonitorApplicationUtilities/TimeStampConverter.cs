using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json.Converters;

namespace MonitorApplication_Utilities
{
    public class TimeStampConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        public override bool CanConvert(Type objectType)
        {
            return IsNumericType(objectType);
        }

        public override object ReadJson(
            JsonReader reader, 
            Type objectType, 
            object existingValue, 
            JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            DateTime dateTime = _epoch.AddMilliseconds((long) reader.Value).ToLocalTime();
            return dateTime;
        }

        public override void WriteJson(
            JsonWriter writer, 
            object value, 
            JsonSerializer serializer)
        {
            DateTime dateTime = (DateTime)value;
            string valueToWrite = (dateTime - _epoch).TotalMilliseconds.ToString(CultureInfo.InvariantCulture);

            writer.WriteRawValue(valueToWrite);
        }

        private bool IsNumericType(Type type) {
            switch (Type.GetTypeCode(type))
            { 
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                return true;
            default:
                return false;
            }
        }

        protected static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime myDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(unixTimeStamp / TicsDenomintor);
            DateTime localDate = myDate
                .ToLocalTime();
            return localDate;
        }
        private const int TicsDenomintor = 1000;
    }
}
