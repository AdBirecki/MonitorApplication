using System;
using System.Collections;

namespace MonitorApplication_SL
{
    public class GoldDataDTO
    {
        public long ts { get; set; }
        public long tsj { get; set; }
        public DateTime date { get; set;}
        public IEnumerable items { get; set; }
    }
}
