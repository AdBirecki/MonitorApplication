using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using MonitorApplication_Utilities;

namespace MonitorApplication_Models
{
    public class GoldDataDto
    {
        [JsonProperty(TimeStampName)]
        public long TimeStamp { get; set; }


        [JsonProperty(TimeStampJName)]
        public long TimeStampJ { get; set; }

        [JsonConverter(typeof(DateFormatConverter<string>))]
        public DateTime date { get; set; }

        [JsonProperty(ItemsArray)]
        public IEnumerable<ChangeDto> Children { get; set; }

        private const string TimeStampName = "ts";
        private const string TimeStampJName = "tsj";
        private const string ItemsArray = "items";
    }
    

}
