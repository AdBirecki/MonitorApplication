using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using MonitorApplicationUtilities;
using MonitorApplication_Utilities;

namespace MonitorApplication_Models
{
    public class GoldDataDto
    {
        [JsonProperty(TimeStampName)]
        [JsonConverter(typeof(TimeStampConverter))]
        public DateTime TimeStamp { get; set; }

        [JsonProperty(TimeStampJName)]
        [JsonConverter(typeof(TimeStampConverter))]
        public DateTime TimeStampJ { get; set; }

        [JsonConverter(typeof(DateFormatConverter<string>))]
        public DateTime date { get; set; }

        public IEnumerable<ChangeDto> items { get; set; }

        private const string TimeStampName = "ts";
        private const string TimeStampJName = "tsj";
    }
    

}
