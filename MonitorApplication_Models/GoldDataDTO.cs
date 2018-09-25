using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using MonitorApplicationUtilities;

namespace MonitorApplication_Models
{
    public class GoldDataDTO
    {
        public long ts { get; set; }
        public long tsj { get; set; }
        //[JsonConverter(typeof(DateFormatConverter), "MMM ddd YYYY, hh:mm:ss tt Z")]
        public string date { get; set; }
        public IEnumerable<ChangeDTO> items { get; set; }
    }
}
