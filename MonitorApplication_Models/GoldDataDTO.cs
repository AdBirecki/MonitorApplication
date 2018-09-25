using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using MonitorApplicationUtilities;
using MonitorApplication_Utilities;

namespace MonitorApplication_Models
{
    public class GoldDataDTO
    {
        [JsonConverter(typeof(TimeStampConverter))]
        public DateTime ts { get; set; }
        [JsonConverter(typeof(TimeStampConverter))]
        public DateTime tsj { get; set; }
        //[JsonConverter(typeof(DateFormatConverter), "MMM ddd YYYY, hh:mm:ss tt Z")]
        public string date { get; set; }
        public IEnumerable<ChangeDTO> items { get; set; }
    }
}
