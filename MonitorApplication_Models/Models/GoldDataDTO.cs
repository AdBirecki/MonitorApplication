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

        [JsonConverter(typeof(TimeStampConverter))]
        [JsonProperty(TimeStampName)]
        public DateTime TimeStamp { get; set; }


        [JsonConverter(typeof(TimeStampConverter))]
        [JsonProperty(TimeStampJName)]
        public DateTime TimeStampJ { get; set; }

        [JsonConverter(typeof(DateFormatConverter<string>))]
        public DateTime date { get; set; }

        [JsonProperty("items")]
        public IEnumerable<ChangeDto> children { get; set; }

        private const string TimeStampName = "ts";
        private const string TimeStampJName = "tsj";
    }
    

}
