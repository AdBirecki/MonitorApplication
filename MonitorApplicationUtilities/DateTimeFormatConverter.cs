using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MonitorApplication_Utilities
{
    public class DateTimeFormatConverter : JsonConverter<string>
    {   
        private string[] Parameters { get; set; }
        public DateTimeFormatConverter(params string[] parameters)
        {
            Parameters = parameters;
        }

        public override void WriteJson(JsonWriter writer, 
            string value, 
            JsonSerializer serializer)
        {
            DateTime.TryParse(value, out DateTime result);
            JToken t = JToken.FromObject(result);
            t.WriteTo(writer);
        }

        public override string ReadJson(JsonReader reader, 
            Type objectType, 
            string existingValue, 
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            string foramtedDate = ParseCustomDateFormat(existingValue).ToString(CultureInfo.InvariantCulture);
        
            return foramtedDate;
        }

        private DateTime ParseCustomDateFormat(string existingValue)
        {
            return new DateTime(2018, 9, 26, 10, 34, 23, DateTimeKind.Utc);
        }
    }
}
