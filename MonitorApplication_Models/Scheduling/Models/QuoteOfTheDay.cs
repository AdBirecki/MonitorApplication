using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MonitorApplication_Models.Scheduling.Models
{
    public class QuoteOfTheDayTask : IScheduledTask
    {
        public string Schedule => "* */6 * * *";
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpClient httpClient = new HttpClient();

            var quoteJson = JObject.Parse(await httpClient.GetStringAsync("http://quotes.rest/qod.json"));

            QuoteOftheDay.Current = JsonConvert.DeserializeObject<QuoteOftheDay>(quoteJson["contents"]["quotes"][0].ToString());
        }

    }

    public class QuoteOftheDay
    {
        public static QuoteOftheDay Current { get; set; }
        public string Quote { get; set; }
        public string Author { get; set; }

        static QuoteOftheDay()
        {
            Current = new QuoteOftheDay { Quote = "No quote", Author = "Maarten" };
        }
    }
}
