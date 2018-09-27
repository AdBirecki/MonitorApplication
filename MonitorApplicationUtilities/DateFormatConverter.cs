﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Rewrite.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MonitorApplicationUtilities
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
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
        private DateTime ParseDateString(string dateString)
        {
            string[] SplitStrings = dateString.Split(new char[] { space });
        
            int monthNumber = getMonth(SplitStrings[0]);
            int dayNumber = getDay(SplitStrings[1]);
            int yearNumber = getYear(SplitStrings[2]);

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
