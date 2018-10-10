using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace MonitorApplication_Utilities
{
    public class DateFormatConverter<T> : JsonConverter<DateTime>
    {
        private string DateTimeFormat { get; set; }
        public DateFormatConverter()
        {
        }
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            DateTime dateTime = ParseDateString(reader.Value.ToString());
            return dateTime;
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        private DateTime ParseDateString(string dateString)
        {
            string[] splitStrings = dateString.Split(new char[] { space });
        
            int monthNumber = getMonth(splitStrings[0]);
            int dayNumber = getDay(splitStrings[1]);
            int yearNumber = getYear(splitStrings[2]);

            return new DateTime();
        }

        private int getMonth(string shortName) {
            if (shortName.Contains(space)) 
                {
                shortName = shortName.Remove(space);
            }


            int monthNumber = Array.FindIndex(ShortMonths, item => item.Equals(shortName));
            return monthNumber;
        }

        private int getYear(string year) {
            int.TryParse(year, out int yearInt);
            return yearInt;
        }

        private int getDay(string dayFull) {
            MatchCollection matchList = Regex.Matches(dayFull, @"[0-9]+");
            List<string> list = matchList
                .Cast<Match>()
                .Select(match => match.Value).ToList();

            int.TryParse(list.First(), out int day);
            return day;
        }

        private readonly string[] ShortMonths = {
            "Jan", "Feb", "Mar",
            "Apr", "May", "Jun",
            "Jul", "Aug", "Sep",
            "Oct", "Nov", "Dec" };

        private readonly string[] DayEndings = {
            "st", "nd", "rd", "th"
        };

        private static char space = ' ';
    }
}
