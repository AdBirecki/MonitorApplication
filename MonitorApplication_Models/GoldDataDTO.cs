using System;
using System.Collections;
using System.Collections.Generic;

namespace MonitorApplication_Models
{
    public class GoldDataDTO
    {
        public long ts { get; set; }
        public long tsj { get; set; }
        public string date { get; set; }
        public IEnumerable<ChangeDTO> items { get; set; }
    }
}
