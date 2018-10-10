using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using MonitorApplicationUtilities;
using MonitorApplication_Utilities;

namespace MonitorApplication_Models
{
    public class GoldDataDto
    {
        [JsonConverter(typeof(TimeStampConverter))]
        public DateTime ts { get; set; }
        [JsonConverter(typeof(TimeStampConverter))]
        public DateTime tsj { get; set; }
        [JsonConverter(typeof(DateFormatConverter<string>))]
        public DateTime date { get; set; }
        public IEnumerable<ChangeDTO> items { get; set; }
    }
}
