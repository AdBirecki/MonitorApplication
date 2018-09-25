using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_Utilities
{
    public class TimeStampConverter : JsonConverter
    {
        public TimeStampConverter() {

        }

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

            long tiemstampValue = (long)reader.Value;
            DateTime dateTime = UnixTimeStampToDateTime(tiemstampValue);
            return dateTime;
        }

        public override void WriteJson(JsonWriter writer, 
            object value, 
            JsonSerializer serializer)
        {
            DateTime dateTime = (DateTime)value;
            long tics = dateTime.Ticks * ticsDenomintor;
            JToken t = JToken.FromObject(tics);
            t.WriteTo(writer);
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
                .AddSeconds(unixTimeStamp / ticsDenomintor);
            DateTime localDate = myDate
                .ToLocalTime();
            return localDate;
        }
        private const int ticsDenomintor = 1000;
    }
}
