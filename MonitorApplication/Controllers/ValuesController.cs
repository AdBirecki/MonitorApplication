using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonitorApplication.Controllers.BaseControllers;
using MonitorApplication.HttpClient;
using MonitorApplication_Models;
using Newtonsoft.Json;
using RestSharp;

namespace MonitorApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ApiBaseController
    {
        private readonly string address;
        private readonly GoldClient _goldClient;
        public ValuesController(IConfiguration configuration, GoldClient goldClient)
        {
            _goldClient = goldClient;

        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult>  Get()
        {
           
            IRestClient client = new RestClient("https://data-asg.goldprice.org/");
            IRestRequest request = new RestRequest("dbXRates/{currency}", Method.GET);
            request.AddUrlSegment("currency", "USD");

            string data = await _goldClient.GetGoldValues();
            GoldDataDTO goldData2 = JsonConvert.DeserializeObject<GoldDataDTO>(data);
            IRestResponse<GoldDataDTO> response = client.Execute<GoldDataDTO>(request);           
            GoldDataDTO goldData = JsonConvert.DeserializeObject<GoldDataDTO>(response.Content);
           //  DateTime dt = UnixTimeStampToDateTime(goldData.ts);

            return Ok(response.Data);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
