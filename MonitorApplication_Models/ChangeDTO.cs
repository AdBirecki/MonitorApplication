using System.Runtime.Serialization;
using Newtonsoft.Json;
namespace MonitorApplication_Models
{
    public class ChangeDto
    {
        [DataMember(Name = "curr")]
        public string Currency { get; set; }

        [JsonProperty("xauPrice")]
        public double XauPrice { get; set; }

        [JsonProperty("xagPrice")]
        public double XagPrice { get; set; }

        [JsonProperty("chgXau")]
        public double ChgXau { get; set; }

        [JsonProperty("chgXag")]
        public double ChgXag { get; set; }

        [DataMember(Name = "pcXau")]
        public double PcXau { get; set; }

        [JsonProperty("pcXag")]
        public double PcXag { get; set; }

        [JsonProperty("xauClose")]
        public double XauClose { get; set; }

        [JsonProperty("xagClose")]
        public double XagClose { get; set; }
    }
}
